using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

public class ClientesMvcController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ClientesMvcController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Login() => View();

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string Email, string Password)
    {
        var client = _httpClientFactory.CreateClient();
        var body = JsonSerializer.Serialize(new
        {
            email = Email,
            password = Password
        });
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:7194/api/Clientes/login", content);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(json);
            var token = result.GetProperty("token").GetString();
            HttpContext.Session.SetString("JwtToken", token!);
            return RedirectToAction("Perfil");
        }

        ViewBag.Error = "Credenciales inválidas.";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string Nombre, string Email, string Telefono, string Password)
    {
        var client = _httpClientFactory.CreateClient();
        var body = JsonSerializer.Serialize(new
        {
            nombre = Nombre,
            email = Email,
            telefono = Telefono,
            password = Password
        });
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:7194/api/Clientes/register", content);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Login");

        var error = await response.Content.ReadAsStringAsync();
        ViewBag.Error = "Error al registrarse. Verifica los datos.";
        return View();
    }

    public async Task<IActionResult> Perfil()
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
            return RedirectToAction("Login");

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("https://localhost:7194/api/Clientes/perfil");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var perfil = JsonSerializer.Deserialize<JsonElement>(json);
            ViewBag.NombreCliente = perfil.GetProperty("nombre").GetString();
            ViewBag.EmailCliente = perfil.GetProperty("email").GetString();
            ViewBag.TelefonoCliente = perfil.TryGetProperty("telefono", out var tel) ? tel.GetString() : "—";
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ActualizarPerfil(string Nombre, string Email, string Telefono)
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
            return RedirectToAction("Login");

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var body = JsonSerializer.Serialize(new
        {
            nombre = Nombre,
            email = Email,
            telefono = Telefono
        });
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:7194/api/Clientes/actualizar-perfil", content);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Perfil");

        ViewBag.Error = "Error al actualizar el perfil.";
        return View();
    }

    public async Task<IActionResult> ActualizarPerfil()
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
            return RedirectToAction("Login");

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("https://localhost:7194/api/Clientes/perfil");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var perfil = JsonSerializer.Deserialize<JsonElement>(json);
            ViewBag.NombreCliente = perfil.GetProperty("nombre").GetString();
            ViewBag.EmailCliente = perfil.GetProperty("email").GetString();
            ViewBag.TelefonoCliente = perfil.TryGetProperty("telefono", out var tel) ? tel.GetString() : "—";
        }

        return View();
    }

    public IActionResult CambiarPassword() => View();

    [HttpPost]
    public async Task<IActionResult> CambiarPassword(string PasswordActual, string NuevaPassword, string ConfirmarPassword)
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
            return RedirectToAction("Login");

        if (NuevaPassword != ConfirmarPassword)
        {
            ViewBag.Error = "Las contraseñas no coinciden.";
            return View();
        }

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var body = JsonSerializer.Serialize(new
        {
            passwordActual = PasswordActual,
            passwordNueva = NuevaPassword
        });
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:7194/api/Clientes/cambiar-password", content);

        if (response.IsSuccessStatusCode)
        {
            ViewBag.Success = "Contraseña actualizada correctamente.";
            return View();
        }

        ViewBag.Error = "Error al cambiar la contraseña. Verifica tu contraseña actual.";
        return View();
    }
}