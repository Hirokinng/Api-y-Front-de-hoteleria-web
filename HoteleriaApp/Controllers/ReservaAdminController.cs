using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoteleriaApp.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Cookies")]
    public class ReservaAdminController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Gestión de Reservas";
            return View();
        }

        public IActionResult Detalle(Guid id)
        {
            ViewData["Title"] = "Detalle de Reserva";
            ViewBag.ReservaId = id;
            return View();
        }
    }
}