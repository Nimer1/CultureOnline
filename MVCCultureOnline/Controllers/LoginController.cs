using Microsoft.AspNetCore.Mvc;
using MVCCultureOnline.Models;

namespace MVCCultureOnline.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("login")]
        public IActionResult Login(LoginViewModel login)
        {
            // Here you would typically validate the user credentials
            // For demonstration purposes, we'll just redirect to a welcome page
            if (login == null)
            {
                return View("Error"); 
                    
            }
            if(login.User.Equals("admin") && login.Password.Equals("123456"))
            {
                // Redirect to a welcome page or dashboard
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Error en el acceso";
                return View("Index");
            }
        }
    }
}
