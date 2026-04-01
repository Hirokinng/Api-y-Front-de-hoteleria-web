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

        public async Task<IActionResult> Index(string? hotel)
        {
            var pisosDb = await _pisoService.GetAllAsync();

            // Si no se selecciona un hotel, mostramos la vista de "Carpetas/Edificios"
            if (string.IsNullOrWhiteSpace(hotel))
            {
                // Agrupamos los pisos por su Nombre Clave para crear las carpetas
                var carpetas = pisosDb
                    .GroupBy(p => string.IsNullOrWhiteSpace(p.NombreClave) ? "Sin Asignar" : p.NombreClave)
                    .ToDictionary(g => g.Key, g => g.Count());

                ViewBag.VistaActual = "Carpetas";
                ViewBag.Carpetas = carpetas;

                return View("~/Views/Piso/Index.cshtml", new List<PisoDto>());
            }

            // Si se hizo clic en un hotel, mostramos la tabla con sus pisos
            var pisosFiltrados = hotel == "Sin Asignar"
                ? pisosDb.Where(p => string.IsNullOrWhiteSpace(p.NombreClave)).ToList()
                : pisosDb.Where(p => p.NombreClave == hotel).ToList();

            var pisosDto = pisosFiltrados.Select(p => new PisoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                numeroPiso = p.NumeroPiso,
                NombreClave = p.NombreClave
            }).ToList();

            ViewBag.VistaActual = "Pisos";
            ViewBag.HotelActual = hotel;

            return View("~/Views/Piso/Index.cshtml", pisosDto);
        }
        // GET: Muestra la pantalla del formulario vacío
        public IActionResult Create()
        {
            return View("~/Views/Piso/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PisoDto dto)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Piso/Create.cshtml", dto);

            var opciones = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var json = System.Text.Json.JsonSerializer.Serialize(dto);
            var nuevoPiso = System.Text.Json.JsonSerializer.Deserialize<HoteleriaApp.Core.Domain.Entities.Piso>(json, opciones)!;

            await _pisoService.CreateAsync(nuevoPiso);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            var piso = await _pisoService.GetByIdAsync(id);
            if (piso == null) return NotFound();

            var dto = new PisoDto
            {
                Id = piso.Id,
                Nombre = piso.Nombre,
                Descripcion = piso.Descripcion,
                numeroPiso = piso.NumeroPiso
            };

            return View("~/Views/Piso/Edit.cshtml", dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PisoDto dto)
        {
            if (id != dto.Id) return BadRequest();

            if (!ModelState.IsValid)
                return View("~/Views/Piso/Edit.cshtml", dto);

            await _pisoService.UpdateAsync(id, dto.Nombre, dto.Descripcion, dto.numeroPiso, dto.NombreClave);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            await _pisoService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}