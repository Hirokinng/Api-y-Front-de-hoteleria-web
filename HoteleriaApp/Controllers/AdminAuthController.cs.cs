using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace HoteleriaApp.Controllers
{
    public class AdminAuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminAuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Login()
        {
            // Si ya está autenticado como Admin, redirigir al dashboard
            if (User.Identity?.IsAuthenticated == true && User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            var client = _httpClientFactory.CreateClient();
            var body = JsonSerializer.Serialize(new { email = Email, password = Password });
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7194/api/Auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Credenciales inválidas o no tienes permisos de administrador.";
                return View();
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(json);
            var token = result.GetProperty("token").GetString();
            var rol = result.GetProperty("rol").GetString();
            var nombre = result.GetProperty("nombre").GetString();

            if (rol != "Admin")
            {
                ViewBag.Error = "No tienes permisos de administrador.";
                return View();
            }

            // Guardar token en Session
            HttpContext.Session.SetString("JwtToken", token!);
            HttpContext.Session.SetString("AdminNombre", nombre!);

            // Cookie de autenticación con rol Admin
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Email),
                new Claim(ClaimTypes.GivenName, nombre!),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8) });

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("JwtToken");
            HttpContext.Session.Remove("AdminNombre");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}