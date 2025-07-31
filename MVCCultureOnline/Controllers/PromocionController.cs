using CultureOnline.Application.DTOs;
using CultureOnline.Application.Services.Implementations;
using CultureOnline.Application.Services.Interfaces;
using CultureOnline.Infraestructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace MVCCultureOnline.Controllers
{
    public class PromocionController : Controller
    {
        private readonly IServicePromocion _servicePromociones;
        private readonly IServiceTipoPromocion _serviceTipoPromocion;
        private readonly IServiceProducto _serviceProducto;
        private readonly IServiceCategoria _serviceCategoria;

        public PromocionController(IServicePromocion servicePromociones, IServiceTipoPromocion serviceTipoPromocion, IServiceProducto serviceProducto, IServiceCategoria serviceCategoria)
        {
            _servicePromociones = servicePromociones;
            _serviceTipoPromocion = serviceTipoPromocion;
            _serviceProducto = serviceProducto;
            _serviceCategoria = serviceCategoria;
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

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            try
            {
                await CargarCombosAsync();
                return View("Crear", new PromocionDTO());
            }catch(Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(PromocionDTO promocion)
        {
            try
            {
                await CargarCombosAsync();
                ValidarPromocion(promocion);
                if (!ModelState.IsValid)
                return View("Crear", promocion);
                await _servicePromociones.AddAsync(promocion);
            return RedirectToAction("Index");
            }catch(Exception ex)
            { 
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var promocion = await _servicePromociones.FindByIdAsync(id);
            if (promocion == null) return NotFound();
            await CargarCombosAsync(promocion); 
            return View("Editar", promocion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(PromocionDTO promocion)
        {
            await CargarCombosAsync(promocion);
            ValidarPromocion(promocion);
            if (!ModelState.IsValid)
                return View("Editar", promocion);
            await _servicePromociones.UpdateAsync(promocion.IdPromocion, promocion);

            var tipos = (await _serviceTipoPromocion.ListAsync())
          .Where(t => t.IdTipoPromocion == 5 || t.IdTipoPromocion == 6)
          .ToList();
            ViewBag.TiposPromocion = new SelectList(tipos, "IdTipoPromocion", "Descripcion", promocion.TipoPromocionId);
   

            return RedirectToAction("Index");
        }

        private async Task CargarCombosAsync()
        {
            var tipos = (await _serviceTipoPromocion.ListAsync())
                .Where(t => t.IdTipoPromocion == 5 || t.IdTipoPromocion == 6)
                .ToList();
            ViewBag.TiposPromocion = new SelectList(tipos, "IdTipoPromocion", "Descripcion");
            var categorias = await _serviceCategoria.ListAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");
            var productos = await _serviceProducto.ListAsync();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");
        }

        // Sobrecarga de CargarCombosAsync
        private async Task CargarCombosAsync(PromocionDTO model)
        {
            var tipos = (await _serviceTipoPromocion.ListAsync())
                .Where(t => t.IdTipoPromocion == 5 || t.IdTipoPromocion == 6)
                .ToList();
            ViewBag.TiposPromocion = new SelectList(tipos, "IdTipoPromocion", "Descripcion", model.TipoPromocionId);
            var categorias = await _serviceCategoria.ListAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre", model.CategoriaId);
            var productos = await _serviceProducto.ListAsync();
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre", model.ProductoId);
       
        }

        private void ValidarPromocion(PromocionDTO promocion)
        {
           
            if (string.IsNullOrWhiteSpace(promocion.Nombre))
                ModelState.AddModelError("Nombre", "El nombre de la promoción es obligatorio.");
            if (promocion.FechaInicio < DateTime.Today)
                ModelState.AddModelError("FechaInicio", "La fecha de inicio no puede ser anterior a hoy.");
            if (promocion.FechaFin < promocion.FechaInicio)
                ModelState.AddModelError("FechaFin", "La fecha de fin no puede ser anterior a la de inicio.");
            if (promocion.DescuentoPorcentaje <= 0)
                ModelState.AddModelError("DescuentoPorcentaje", "El descuento debe ser mayor a cero.");
            if (promocion.TipoPromocionId == 1 && !promocion.CategoriaId.HasValue)
                ModelState.AddModelError("CategoriaId", "Debe seleccionar una categoría.");
            if (promocion.TipoPromocionId == 2 && !promocion.ProductoId.HasValue)
                ModelState.AddModelError("ProductoId", "Debe seleccionar un producto.");
            
            if (promocion.TipoPromocionId == 5) // Producto
                promocion.CategoriaId = null;
            else if (promocion.TipoPromocionId == 6) // Categoría
                promocion.ProductoId = null;
        }
    }
}

