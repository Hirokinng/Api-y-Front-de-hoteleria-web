using HoteleriaApp.Core.Application.DTOs.Reservas;
using HoteleriaApp.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HoteleriaApp.Controllers
{
    [Authorize]
    public class ReservasController : Controller
    {
        private readonly IReservaService _reservaService;

        public ReservasController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        // LISTAR
        public async Task<IActionResult> Index()
        {
            var reservas = await _reservaService.GetAllAsync();
            return View(reservas);
        }

        // CREAR (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREAR (POST)
        [HttpPost]
        public async Task<IActionResult> Create(CrearReservaDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var idCliente = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var resultado = await _reservaService.CrearAsync(dto, idCliente);

            if (!resultado.Ok)
            {
                ModelState.AddModelError("", resultado.Error!);
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        // EDITAR (GET)
        public async Task<IActionResult> Edit(Guid id)
        {
            var reserva = await _reservaService.GetByIdAsync(id);

            if (reserva == null) return NotFound();

            ViewBag.Id = id;

            var dto = new EditarReservaDto
            {
                FechaEntrada = reserva.FechaEntrada,
                FechaSalida = reserva.FechaSalida,
                NumeroHuespedes = reserva.NumeroHuespedes,
                IdsServicios = new List<Guid>()
            };

            return View(dto);
        }

        // EDITAR (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, EditarReservaDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var resultado = await _reservaService.EditarAsync(id, dto);

            if (!resultado.Ok)
            {
                ModelState.AddModelError("", resultado.Error!);
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        // ELIMINAR
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _reservaService.CancelarAsync(id);
            return RedirectToAction("Index");
        }

        //DETALLES
        public async Task<IActionResult> Detalle(Guid id)
        {
            var reserva = await _reservaService.GetByIdAsync(id);

            if (reserva == null)
                return NotFound();

            return View(reserva);
        }
    }
}