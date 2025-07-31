using CultureOnline.Application.DTOs;
using CultureOnline.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCCultureOnline.Views.ViewModels;

namespace MVCCultureOnline.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IServiceUsuario _serviceUsuario;
        private readonly IServiceRolUsuario _serviceRolUsuario;

        public UsuarioController(IServiceUsuario serviceUsuario, IServiceRolUsuario serviceRolUsuario)
        {
            _serviceUsuario = serviceUsuario;
            _serviceRolUsuario = serviceRolUsuario;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceUsuario.ListAsync();
            return View(collection);
        }

        // GET: UsuarioController/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET:  
        public async Task<IActionResult> Login(string id, string password)
        {
            var @object = await _serviceUsuario.LoginAsync(id, password);
            if (@object == null)
            {
                ViewBag.Message = "Error en Login o Password";
                return View("Login");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewModelRegistro model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Mapea el ViewModel a UsuarioDTO
            var dto = new UsuarioDTO
            {
                Nombre = model.Nombre,
                Correo = model.Correo,
                Contrasena = model.Contrasena,
                IDRol = 2,
                Estado = "Activo",
                UltimoInicioSesion = DateTime.Now
            };

            await _serviceUsuario.AddAsync(dto);

            return RedirectToAction("Index", "Login");
        }


        // GET: UsuarioController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var @object = await _serviceUsuario.FindByIdAsync(id);
            return View(@object);
        }

        // GET: UsuarioController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            /*var @object = await _serviceUsuario.FindByIdAsync(id);
            return View(@object);*/
            var usuario = await _serviceUsuario.FindByIdAsync(id);

            var roles = await _serviceRolUsuario.ListAsync();
            ViewBag.Roles = new SelectList(roles, "IDRol", "Descripcion", usuario.IDRol);

            return View(usuario);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioDTO dto)
        {
            await _serviceUsuario.UpdateAsync(id, dto);
            TempData["MensajeExito"] = "Usuario actualizado correctamente";
            return RedirectToAction("Index");

        }

        // GET: UsuarioController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var @object = await _serviceUsuario.FindByIdAsync(id);
            return View(@object);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            await _serviceUsuario.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}