using HoteleriaApp.Core.Application.DTOs;
using HoteleriaApp.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
namespace HoteleriaApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PisoMvcController : Controller
    {
        private readonly IPisoService _pisoService;

        public PisoMvcController(IPisoService pisoService)
        {
            _pisoService = pisoService;
        }

        public async Task<IActionResult> Index()
        {

            var pisosDb = await _pisoService.GetAllAsync();

       
            var pisosDto = pisosDb.Select(p => new PisoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                numeroPiso = p.NumeroPiso
            }).ToList();

            return View("~/Views/Piso/Index.cshtml", pisosDto);
        }
        // GET: Muestra la pantalla del formulario vacío
        public IActionResult Create()
        {
            // Le forzamos la ruta exacta igual que en el Index
            return View("~/Views/Piso/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PisoDto dto)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Piso/Create.cshtml", dto);

            // 1. Instanciamos tu entidad blindada convirtiendo el DTO
            var json = System.Text.Json.JsonSerializer.Serialize(dto);
            var nuevoPiso = System.Text.Json.JsonSerializer.Deserialize<HoteleriaApp.Core.Domain.Entities.Piso>(json)!;

            // 2. Le pasamos ÚNICAMENTE el objeto completo a tu servicio (¡como debe ser!)
            await _pisoService.CreateAsync(nuevoPiso);

            // 3. Redirigimos a la tabla para ver el éxito
            return RedirectToAction(nameof(Index));
        }
    }
}