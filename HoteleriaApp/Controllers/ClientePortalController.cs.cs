using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class ClientePortalController : Controller
{
    public IActionResult Index() => View();
    public IActionResult Buscar() => View();
    public IActionResult Detalle() => View();

    [Authorize(AuthenticationSchemes = "Cookies")]
    public IActionResult MisReservas() => View();
}