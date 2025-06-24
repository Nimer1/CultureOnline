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
            return View("Detalle",producto);
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
    }

}
