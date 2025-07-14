using CultureOnline.Application.DTOs;
using CultureOnline.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MVCCultureOnline.Controllers
{
    public class ReseñaController : Controller
    {
        private readonly IServiceReseña _serviceReseña;
        private readonly IServiceProducto _serviceProducto;
        private readonly IservicePedido _servicePedido;

        public ReseñaController(IServiceReseña serviceReseña, IServiceProducto serviceProducto, IservicePedido servicePedido)
        {
            _serviceReseña = serviceReseña;
            _serviceProducto = serviceProducto;
            _servicePedido = servicePedido;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reseñas = await _serviceReseña.ListAsync();
            var reseñasOrdenadas = reseñas.OrderByDescending(r => r.Fecha);
            return View(reseñasOrdenadas); // Vista: Views/Reseña/Index.cshtml
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            var reseña = await _serviceReseña.FindByIdAsync(id);
            if (reseña == null)
                return NotFound();

            return View("Detalle", reseña); // Vista: Views/Reseña/Detalle.cshtml
        }

        [HttpGet]
        [Authorize] 
        public async Task<IActionResult> Create(int productoId, int pedidoId)
        {
            // Obtener el usuario autenticado
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            int usuarioId = 0;
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out usuarioId))
            {
                return RedirectToAction("Index", "Login");
            }

            // Verificar si ya existe una reseña de este usuario para este producto
            var reseñas = await _serviceReseña.ListAsync();
            var reseñaExistente = reseñas.FirstOrDefault(r => r.ProductoId == productoId && r.UsuarioId == usuarioId);
            if (reseñaExistente != null)
            {
                // Redirigir a la edición de la reseña existente
                return RedirectToAction("Edit", new { id = reseñaExistente.Id });
            }

            // Obtener el producto
            var producto = await _serviceProducto.FindByIdAsync(productoId);
            if (producto == null)
            {
                return NotFound();
            }

            // Obtener todos los pedidos del usuario
            var pedidosUsuario = await _servicePedido.ListAsync();
            var pedidosConProducto = pedidosUsuario
                .Where(p => p.UsuarioId == usuarioId && p.DetallePedido.Any(d => d.ProductoId == productoId))
                .ToList();

            // Si el usuario no ha comprado el producto, no puede crear reseña
            if (!pedidosConProducto.Any())
            {
                TempData["MensajeError"] = "Solo puedes reseñar productos que hayas comprado.";
                return RedirectToAction("Detalle", "Producto", new { id = productoId, error = true });
            }

            // Prepara la lista de pedidos para el dropdown
            ViewBag.ListPedidos = pedidosConProducto;
            ViewBag.NombreProducto = producto.Nombre;

            var reseña = new ReseñaDTO
            {
                ProductoId = productoId,
                PedidoId = pedidoId,
                UsuarioId = usuarioId,
                Fecha = DateTime.Now
            };

            return View(reseña);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReseñaDTO reseña)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);

                if (!ModelState.IsValid)
                {
                
                    if (reseña.ProductoId == 0 && Request.Query.ContainsKey("productoId"))
                    {
                        int.TryParse(Request.Query["productoId"], out int prodId);
                        reseña.ProductoId = prodId;
                    }

                    // Recarga los datos necesarios para la vista
                    var productoReload = await _serviceProducto.FindByIdAsync(reseña.ProductoId);
                    ViewBag.NombreProducto = productoReload?.Nombre ?? "";
                    var pedidosUsuario = await _servicePedido.ListAsync();
                    ViewBag.ListPedidos = pedidosUsuario
                        .Where(p => p.UsuarioId == reseña.UsuarioId && p.DetallePedido.Any(d => d.ProductoId == reseña.ProductoId))
                        .ToList();

                    // Muestra los errores en la vista
                    return View(reseña);
                }

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    reseña.UsuarioId = userId;
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

                reseña.Fecha = DateTime.Now;
                reseña.Aprobada = true;

                await _serviceReseña.AddAsync(reseña);

                // Actualizar el promedio de valoraciones del producto
                var reseñasProducto = await _serviceReseña.ListAsync();
                var reseñasDeEsteProducto = reseñasProducto
                    .Where(r => r.ProductoId == reseña.ProductoId && r.Aprobada == true && r.Valoracion > 0)
                    .ToList();

                double promedio = 0;
                if (reseñasDeEsteProducto.Any())
                {
                    promedio = reseñasDeEsteProducto.Average(r => r.Valoracion);
                }

                var producto = await _serviceProducto.FindByIdAsync(reseña.ProductoId);
                if (producto != null)
                {
                    producto.PromedioValoracion = promedio;
                    var categorias = producto.Categorias ?? new List<CategoriaDTO>();
                    try
                    {
                        await _serviceProducto.UpdateAsync(
                            producto.Id,
                            producto,
                            categorias.Select(c => c.Id.ToString())
                                .ToArray()
                        );
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Error: {ex.Message}");
                    }
                }

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, productoId = reseña.ProductoId });
                }
                return RedirectToAction("Detalle", "Producto", new { id = reseña.ProductoId });
            }
            catch (Exception ex)
            {
                // TEMPORAL: para depuración rápida
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var reseña = await _serviceReseña.FindByIdAsync(id);
            if (reseña == null) return NotFound();
            return View(reseña);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReseñaDTO reseña)
        {
            try
            {
                if (id != reseña.Id) return NotFound();
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Asigna el usuario autenticado
                        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
                        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                        {
                            reseña.UsuarioId = userId;
                        }
                        reseña.Fecha = DateTime.Now;
                        reseña.Aprobada = true;

                        // Asegura que PedidoId no sea 0 ni null
                        if (reseña.PedidoId == 0)
                        {
                            // Busca la reseña original para obtener el PedidoId correcto
                            var reseñaOriginal = await _serviceReseña.FindByIdAsync(id);
                            if (reseñaOriginal != null && reseñaOriginal.PedidoId != 0)
                            {
                                reseña.PedidoId = reseñaOriginal.PedidoId;
                            }
                        }

                        await _serviceReseña.UpdateAsync(id, reseña);

                        // Actualizar el promedio de valoraciones del producto
                        var reseñasProducto = await _serviceReseña.ListAsync();
                        var reseñasDeEsteProducto = reseñasProducto
                            .Where(r => r.ProductoId == reseña.ProductoId && r.Aprobada == true && r.Valoracion > 0)
                            .ToList();

                        double promedio = 0;
                        if (reseñasDeEsteProducto.Any())
                        {
                            promedio = reseñasDeEsteProducto.Average(r => r.Valoracion);
                        }

                        var producto = await _serviceProducto.FindByIdAsync(reseña.ProductoId);
                        if (producto != null)
                        {
                            producto.PromedioValoracion = promedio;
                            var categorias = producto.Categorias ?? new List<CategoriaDTO>();
                            try
                            {
                                await _serviceProducto.UpdateAsync(
                                    producto.Id,
                                    producto,
                                    categorias.Select(c => c.Id.ToString())
                                        .ToArray()
                                );
                            }
                            catch (Exception ex)
                            {
                                return StatusCode(500, $"Error: {ex.Message}");
                            }
                        }

                        return RedirectToAction("Detalle", "Producto", new { id = reseña.ProductoId });
                    }
                    catch (Exception ex)
                    {
                        var errorMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                        ModelState.AddModelError("", $"Error al guardar los cambios: {errorMsg}");
                    }
                }
            }
            catch (Exception ex)
            {
                // TEMPORAL: para depuración rápida
                return StatusCode(500, $"Error: {ex.Message}");
            }
            return View(reseña);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var reseña = await _serviceReseña.FindByIdAsync(id);
            if (reseña == null) return NotFound();
            return View(reseña);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _serviceReseña.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

