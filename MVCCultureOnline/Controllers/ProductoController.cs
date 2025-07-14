using CultureOnline.Application.DTOs;
using CultureOnline.Application.Services.Implementations;
using CultureOnline.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MVCCultureOnline.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IServiceProducto _serviceProducto;
        private readonly IServiceCategoria _serviceCategoria;

        public ProductoController(IServiceProducto service, IServiceCategoria serviceCategoria)
        {
            _serviceProducto = service;
            _serviceCategoria = serviceCategoria;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var productos = await _serviceProducto.ListAsync();
            var categorias = await _serviceCategoria.ListAsync();

            ViewBag.ListCategorias = categorias;
            return View(productos);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            var producto = await _serviceProducto.FindByIdAsync(id);

            if (producto == null)
                return NotFound();

            producto.Reseñas = await _serviceProducto.GetReseñasPorProductoAsync(id);

            return View("Detalle", producto);
        }

        /*[HttpGet]
        public async Task<IActionResult> BuscarPorCategoria(int categoriaId)
        {
            IEnumerable<ProductoDTO> productos;

            if (categoriaId == 0)
                productos = await _serviceProducto.ListAsync();
            else
                productos = await _serviceProducto.GetProductoByCategoria(categoriaId);

            return PartialView("_ListProductos", productos);
        }*/
        public async Task<IActionResult> Promociones()
        {
            var productos = await _serviceProducto.ListAsync();
            // Solo productos con precio promocional válido (calculado en el servicio)
            var productosEnPromocion = productos
                .Where(p => p.PrecioPromocional.HasValue && p.PrecioPromocional.Value < p.Precio)
                .ToList();
            return View(productosEnPromocion);
        }

        [HttpGet]
        public async Task<IActionResult> ReseñasParcial(int productoId)
        {
            var producto = await _serviceProducto.FindByIdAsync(productoId);
            if (producto == null)
                return NotFound();

            return PartialView("_ReseñasProducto", producto.Reseñas.ToList());
        }
    }

}
