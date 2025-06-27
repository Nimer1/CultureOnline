using CultureOnline.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MVCCultureOnline.Controllers
{
    public class ReseñaController : Controller
    {
        private readonly IServiceReseña _serviceReseña;

        public ReseñaController(IServiceReseña serviceReseña)
        {
            _serviceReseña = serviceReseña;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reseñas = await _serviceReseña.ListAsync();
            return View(reseñas); // Vista: Views/Reseña/Index.cshtml
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            var reseña = await _serviceReseña.FindByIdAsync(id);
            if (reseña == null)
                return NotFound();

            return View("Detalle", reseña); // Vista: Views/Reseña/Detalle.cshtml
        }
    }
}

