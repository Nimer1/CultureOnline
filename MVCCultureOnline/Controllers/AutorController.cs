using CultureOnline.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MVCCultureOnline.Controllers
{
    public class AutorController : Controller
    {
        private readonly IServiceAutor _serviceAutor;
        public AutorController(IServiceAutor serviceAutor)
        {
            _serviceAutor = serviceAutor;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceAutor.ListAsync();
            return View(collection);
        }
    }
}
