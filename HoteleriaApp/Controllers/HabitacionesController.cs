using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoteleriaApp.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Cookies")]
    public class HabitacionesController : Controller
    {
      
        public IActionResult Index()
        {
            ViewData["Title"] = "Inventario de Habitaciones";
            return View();
        }

        public IActionResult Disponibilidad()
        {
            ViewData["Title"] = "Disponibilidad";
            return View();
        }

        public IActionResult Asignar()
        {
            ViewData["Title"] = "Asignar Habitación a Reserva";
            return View();
        }

        public IActionResult Bloquear()
        {
            ViewData["Title"] = "Bloquear Habitación";
            return View();
        }
    }
}
