using CultureOnline.Application.Services.Implementations;
using CultureOnline.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MVCCultureOnline.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IservicePedido _servicePedido;

        public PedidoController(IservicePedido servicePedido)
        {
            _servicePedido = servicePedido;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pedidos = await _servicePedido.ListAsync();
            return View(pedidos); // Vista: Views/Pedido/Index.cshtml
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            var pedidos = await _servicePedido.FindByIdAsync(id);
            if (pedidos == null)
                return NotFound();

            return View("Detalle", pedidos); // Vista: Views/Pedido/Detalle.cshtml
        }
    }
}
