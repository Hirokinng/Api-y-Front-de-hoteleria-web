using HoteleriaApp.Core.Application.DTOs.Reservas;
using HoteleriaApp.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HoteleriaApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReservaController : Controller
    {
        private readonly IReservaService _reservaService;

        public ReservaController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        public async Task<IActionResult> Index()
        {
            var reservas = await _reservaService.GetAllAsync();
            return View(reservas);
        }

        public async Task<IActionResult> Detalle(Guid id)
        {
            var reserva = await _reservaService.GetByIdAsync(id);
            if (reserva == null) return NotFound();
            return View(reserva);
        }

        public IActionResult Crear(Guid? idHabitacion)
        {
            if (idHabitacion.HasValue)
                ViewBag.IdHabitacion = idHabitacion.Value;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearReservaDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var idCliente = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _reservaService.CrearAsync(dto, idCliente);
            if (!result.Ok)
            {
                TempData["Error"] = result.Error;
                return View(dto);
            }

            TempData["Exito"] = "Reserva creada exitosamente.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(Guid id)
        {
            var reserva = await _reservaService.GetByIdAsync(id);
            if (reserva == null) return NotFound();

            var dto = new EditarReservaDto
            {
                FechaEntrada = reserva.FechaEntrada,
                FechaSalida = reserva.FechaSalida,
                NumeroHuespedes = reserva.NumeroHuespedes
            };

            ViewBag.ReservaId = id;
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Guid id, EditarReservaDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var result = await _reservaService.EditarAsync(id, dto);
            if (!result.Ok)
            {
                TempData["Error"] = result.Error;
                return View(dto);
            }

            TempData["Exito"] = "Reserva actualizada exitosamente.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(Guid id)
        {
            var result = await _reservaService.CancelarAsync(id);
            if (!result.Ok)
                TempData["Error"] = result.Error;
            else
                TempData["Exito"] = "Reserva cancelada exitosamente.";

            return RedirectToAction("Index");
        }

        public IActionResult Disponibilidad()
        {
            return View((List<HabitacionDisponibleDto>?)null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disponibilidad(Guid idCategoria, DateOnly fechaEntrada, DateOnly fechaSalida, byte numHuespedes)
        {
            var disponibles = await _reservaService.BuscarDisponibilidadAsync(idCategoria, fechaEntrada, fechaSalida, numHuespedes);
            return View(disponibles.ToList());
        }
    }
}
