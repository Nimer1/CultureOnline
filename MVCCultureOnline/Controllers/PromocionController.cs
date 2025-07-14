using CultureOnline.Application.Services.Implementations;
using CultureOnline.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MVCCultureOnline.Controllers
{
    public class PromocionController : Controller
    {
        private readonly IServicePromocion _servicePromociones;

        public PromocionController(IServicePromocion servicePromociones)
        {
            _servicePromociones = servicePromociones;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var promociones = await _servicePromociones.ListAsync();
            return View(promociones); // Views/Promocion/Index.cshtml
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            var promocion = await _servicePromociones.FindByIdAsync(id);
            if (promocion == null)
            {
                return NotFound();
            }
            return View("Detalle", promocion);// Views/Promocion/Detalle.cshtml
        }
    }
}

