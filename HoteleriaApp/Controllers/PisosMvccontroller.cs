using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

[Authorize(Roles = "Admin", AuthenticationSchemes = "Cookies")]
public class PisosMvcController : Controller
{

    private readonly IHttpClientFactory _httpClientFactory;
    private const string BaseUrl = "https://localhost:7194/api/Pisos";

    public PisosMvcController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient GetClient()
    {
        var client = _httpClientFactory.CreateClient();
        var token = HttpContext.Session.GetString("JwtToken");
        if (!string.IsNullOrEmpty(token))
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var client = GetClient();
            var response = await client.GetAsync(BaseUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                ViewBag.Pisos = JsonSerializer.Deserialize<JsonElement>(json);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                     response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                ViewBag.Error = "No tienes permisos para ver los pisos. Se requiere rol de administrador.";
                ViewBag.Pisos = null;
            }
            else
            {
                ViewBag.Error = "No se pudieron cargar los pisos.";
                ViewBag.Pisos = null;
            }
        }
        catch (Exception ex)
        {
            ViewBag.Error = "Error al conectar con la API: " + ex.Message;
            ViewBag.Pisos = null;
        }

        return View();
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(string Nombre, string Descripcion, int NumeroPiso)
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrEmpty(token))
        {
            ViewBag.Error = "Sin token en sesión.";
            return View();
        }

        var client = GetClient();
        var body = JsonSerializer.Serialize(new
        {
            nombre = Nombre,
            descripcion = Descripcion,
            numeroPiso = NumeroPiso
        });
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(BaseUrl, content);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");
        ViewBag.Error = "Error al crear el piso.";
        return View();
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var client = GetClient();
        var response = await client.GetAsync($"{BaseUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var piso = JsonSerializer.Deserialize<JsonElement>(json);
            ViewBag.Piso = piso;
            ViewBag.Id = id;
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid Id, string Nombre, string Descripcion, int NumeroPiso)
    {
        var client = GetClient();
        var body = JsonSerializer.Serialize(new
        {
            nombre = Nombre,
            descripcion = Descripcion,
            numeroPiso = NumeroPiso
        });
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"{BaseUrl}/{Id}", content);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ViewBag.Error = "Error al actualizar el piso.";
        ViewBag.Id = Id;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var client = GetClient();
        await client.DeleteAsync($"{BaseUrl}?id={id}");
        return RedirectToAction("Index");
    }
}