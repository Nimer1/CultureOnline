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

        /*[HttpPost("login")]
        public IActionResult Login(LoginViewModel login)
        {
            // Here you would typically validate the user credentials
            // For demonstration purposes, we'll just redirect to a welcome page
            if (login == null)
            {
                return View("Error"); 
                    
            }
            if(login.User ==("admin") && login.Password ==("123456"))
            {
                // Redirect to a welcome page or dashboard
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Error en el acceso";
                return View("Index");
            }
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(ViewModelLogin viewModelLogin)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join("; ", ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage));

                ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login", $"Error de Acceso: {errors}", SweetAlertMessageType.error);
                _logger.LogInformation($"Error en login de {viewModelLogin.User}, Errores --> {errors}");
                return View("Index");
            }

            var usuarioDTO = await _serviceUsuario.LoginAsync(viewModelLogin.User, viewModelLogin.Password);
            if (usuarioDTO == null)
            {
                ViewBag.Message = "Usuario o contraseña incorrectos";


                // ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login", "Usuario o contraseña incorrectos", SweetAlertMessageType.error);
                _logger.LogInformation($"Error en login de {viewModelLogin.User}, Error --> Usuario o contraseña incorrectos");
                return View("Index");
            }

            
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioDTO.Id.ToString()),
                new Claim(ClaimTypes.Name, usuarioDTO.Nombre),
                new Claim(ClaimTypes.Role, usuarioDTO.IDRolNavigation.Descripcion!)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties);

            _logger.LogInformation($"Conexion correcta de {viewModelLogin.User}");

           
            TempData["Mensaje"] = Util.SweetAlertHelper.Mensaje("Login", "Usuario identificado", SweetAlertMessageType.success);

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