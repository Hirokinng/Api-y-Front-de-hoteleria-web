using HoteleriaApp.Core.Application.DTOs.Reservas;
using HoteleriaApp.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly ICategoryService _categoriaService;
        private readonly IHabitacionesService _habitacionService;

        public ReservasController(IReservaService reservaService, ICategoryService categoriaService,
    IHabitacionesService habitacionService)
        {
            _reservaService = reservaService;
            _categoriaService = categoriaService;
            _habitacionService = habitacionService;

        }

        // LISTAR
        public async Task<IActionResult> Index()
        {
            var reservas = await _reservaService.GetAllAsync();
            return View(reservas);
        }

        // CREAR (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREAR (POST)
        [HttpPost]
        public async Task<IActionResult> Create(CrearReservaDto dto)
        {
            // 1. Validación del modelo
            if (!ModelState.IsValid)
                return View(dto);

            // 2. Cliente fijo (SOLO PARA PRUEBAS)
            var idCliente = Guid.Parse("86913BFC-3AF8-4C4F-FEE7-08DE8AC649EB");

            // 3. Llamar al servicio
            var resultado = await _reservaService.CrearAsync(dto, idCliente);

            // 4. Validar resultado del servicio
            if (!resultado.Ok)
            {
                ModelState.AddModelError("", resultado.Error!);
                return View(dto);
            }

            // 5. Redirección después de crear
            return RedirectToAction("Index"); 
        }

        // EDITAR (GET)
        public async Task<IActionResult> Edit(Guid id)
        {
            var dto = await _reservaService.GetEditarByIdAsync(id);
            if (dto == null) return NotFound();

            ViewBag.Id = id;
            await CargarCombos();
            return View(dto);

        }

        // EDITAR (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, EditarReservaDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                await CargarCombos(); // 🔥
                return View(dto);
            }

            var resultado = await _reservaService.EditarAsync(id, dto);

            if (!resultado.Ok)
            {
                ModelState.AddModelError("", resultado.Error!);
                ViewBag.Id = id;
                await CargarCombos(); // 🔥
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        private async Task CargarCombos()
        {
            var categorias = await _categoriaService.GetAllAsync();
            var habitaciones = await _habitacionService.ObtenerInventarioAsync();

            ViewBag.Categorias = new SelectList(categorias ?? Enumerable.Empty<object>(), "Id", "Name");
            ViewBag.Habitaciones = new SelectList(habitaciones ?? Enumerable.Empty<object>(), "Id", "Numero");
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