using HoteleriaApp.Core.Application.Interfaces;
using HoteleriaApp.Core.Domain.Entities;
using HoteleriaApp.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HoteleriaApp.Controllers.Api
{
    [ApiController]
    [Route("api/habitaciones")]
    public class HabitacionesApiController : ControllerBase
    {
        private readonly IHabitacionesService _habitacionesService;
        private readonly IApplicationDbContext _context;

        public HabitacionesApiController(IHabitacionesService habitacionesService, IApplicationDbContext context)
        {
            _habitacionesService = habitacionesService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Habitacion>>> GetInventario()
        {
            var habitaciones = await _habitacionesService.ObtenerInventarioAsync();
            return Ok(habitaciones);
        }

        [HttpGet("tipos")]
        public async Task<ActionResult<List<TipoHabitacion>>> GetTipos()
        {
            var tipos = await _context.TiposHabitacion.ToListAsync();
            return Ok(tipos);
        }

        [HttpGet("amenities")]
        public async Task<ActionResult<List<Amenity>>> GetAmenities()
        {
            var amenities = await _context.Amenities.ToListAsync();
            return Ok(amenities);
        }

        [HttpGet("reservas")]
        public async Task<ActionResult<List<Reserva>>> GetReservas()
        {
            var reservas = await _context.Reservas.ToListAsync();
            return Ok(reservas);
        }

        [HttpGet("disponibles")]
        public async Task<ActionResult<List<Habitacion>>> GetDisponibles(
            [FromQuery] DateOnly fechaInicio,
            [FromQuery] DateOnly fechaFin,
            [FromQuery] Guid? tipoHabitacionId,
            [FromQuery] int? capacidadMinima,
            [FromQuery] List<Guid>? amenitiesIds)
        {
            var disponibles = await _habitacionesService.BuscarDisponiblesAsync(
                fechaInicio,
                fechaFin,
                tipoHabitacionId,
                capacidadMinima,
                amenitiesIds);

            return Ok(disponibles);
        }

        public class AsignarHabitacionRequest
        {
            public Guid ReservaId { get; set; }
            public Guid HabitacionId { get; set; }
        }

        [HttpPost("asignar")]
        public async Task<IActionResult> AsignarHabitacion([FromBody] AsignarHabitacionRequest request)
        {
            var resultado = await _habitacionesService.AsignarHabitacionAReservaAsync(
                request.ReservaId,
                request.HabitacionId);

            if (!resultado)
            {
                return BadRequest(new { message = "No se pudo asignar la habitación a la reserva (verificar capacidad, disponibilidad o datos)." });
            }

            return Ok(new { message = "Habitación asignada correctamente." });
        }

        public class BloqueoHabitacionRequest
        {
            public Guid HabitacionId { get; set; }
            public DateOnly FechaInicio { get; set; }
            public DateOnly FechaFin { get; set; }
            public string Motivo { get; set; } = null!;
            public TipoBloqueoHabitacion Tipo { get; set; }
        }

        [HttpPost("bloquear")]
        public async Task<ActionResult<BloqueoHabitacion>> BloquearHabitacion([FromBody] BloqueoHabitacionRequest request)
        {
            var bloqueo = await _habitacionesService.CrearBloqueoHabitacionAsync(
                request.HabitacionId,
                request.FechaInicio,
                request.FechaFin,
                request.Motivo,
                request.Tipo);

            if (bloqueo is null)
            {
                return BadRequest(new { message = "No se pudo crear el bloqueo (verificar fechas, reservas o bloqueos existentes)." });
            }

            return Ok(bloqueo);
        }
    }
}
