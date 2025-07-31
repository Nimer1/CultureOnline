using CultureOnline.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVCCultureOnline.Models;
using MVCCultureOnline.Util;
using MVCCultureOnline.Views.ViewModels;
using System.Security.Claims;

namespace MVCCultureOnline.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServiceUsuario _serviceUsuario;
        private readonly ILogger<LoginController> _logger;
        public LoginController(IServiceUsuario serviceUsuario, ILogger<LoginController> logger)
        {
            _serviceUsuario = serviceUsuario;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.NotificationMessage = TempData["Mensaje"];
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(ViewModelLogin viewModelLogin)
        {
            if (!ModelState.IsValid)
            {
                // Aquí se recopilan todos los errores de validación del modelo
                string errors = string.Join("; ", ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage));

                // Configura el mensaje de error para mostrar en la vista
                ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login", $"Error de Acceso: {errors}", SweetAlertMessageType.error);
                // Se registra el intento fallido en logs
                _logger.LogInformation($"Error en login de {viewModelLogin.User}, Errores --> {errors}");
                // Retorna a la vista Index con los errores
                return View("Index");
            }

            // --- Autenticación del usuario ---
            var usuarioDTO = await _serviceUsuario.LoginAsync(viewModelLogin.User, viewModelLogin.Password);

            // Primera validación de usuario o contraseña incorrectas
            if (usuarioDTO == null)
            {
                ViewBag.Message = "Usuario o contraseña incorrectos";


                // ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login", "Usuario o contraseña incorrectos", SweetAlertMessageType.error);
                _logger.LogInformation($"Error en login de {viewModelLogin.User}, Error --> Usuario o contraseña incorrectos");
                return View("Index");
            }

            // Usuario no encontrado
            if (usuarioDTO == null)
            {
                ViewBag.Message = "Usuario o contraseña incorrectos";
                _logger.LogInformation($"Error en login de {viewModelLogin.User}, Error --> Usuario o contraseña incorrectos");
                return View("Index");
            }

            // --- Validación del estado del usuario ---
            // Verificar si el usuario está inactivo
            if (usuarioDTO.Estado?.ToLower() == "inactivo")
            {
                ViewBag.Message = "Este correo ya no es válido. Por favor, cree una cuenta nueva con otro correo.";

                _logger.LogInformation($"Intento de login con usuario inactivo: {viewModelLogin.User}");
                return View("Index");
            }

            // --- Actualizar el último inicio de sesión ---
            // Se registra la fecha/hora actual como último inicio de sesión
            usuarioDTO.UltimoInicioSesion = DateTime.Now;
            await _serviceUsuario.ActualizarUltimoInicioSesionAsync(usuarioDTO.Id);

            // --- Creación de la identidad del usuario ---
            // Se configuran los claims (información del usuario) para la cookie de autenticación
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuarioDTO.Nombre),
                new Claim(ClaimTypes.Email, usuarioDTO.Correo),
                new Claim(ClaimTypes.NameIdentifier, usuarioDTO.Id.ToString())
            };

            // Crear identidad y principal para la autenticación
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            _logger.LogInformation($"Conexion correcta de {viewModelLogin.User}");

            TempData["Mensaje"] = Util.SweetAlertHelper.Mensaje("Login", "Usuario identificado", SweetAlertMessageType.success);

            // Redirigir al home después de login exitoso
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }

}