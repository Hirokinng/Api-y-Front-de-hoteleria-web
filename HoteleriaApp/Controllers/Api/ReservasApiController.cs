using HoteleriaApp.Core.Application.DTOs.Reservas;
using HoteleriaApp.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HoteleriaApp.Controllers.Api
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [Authorize]                                    
    public class ReservaApiController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservaApiController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ReservaDto>>> GetAll()
        {
            var reservas = await _reservaService.GetAllAsync();
            return Ok(reservas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaDto>> GetById(Guid id)
        {
            var reserva = await _reservaService.GetByIdAsync(id);
            if (reserva == null)
                return NotFound();
            return Ok(reserva);
        }

        [HttpGet("disponibilidad")]
        public async Task<ActionResult<IReadOnlyList<HabitacionDisponibleDto>>> BuscarDisponibilidad(
            [FromQuery] Guid idCategoria,
            [FromQuery] DateOnly fechaEntrada,
            [FromQuery] DateOnly fechaSalida,
            [FromQuery] byte numHuespedes)
        {
            var disponibles = await _reservaService.BuscarDisponibilidadAsync(
                idCategoria, fechaEntrada, fechaSalida, numHuespedes);
            return Ok(disponibles);
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearReservaDto dto)
        {
            var idCliente = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!); 
            var resultado = await _reservaService.CrearAsync(dto, idCliente);
            if (!resultado.Ok)
                return BadRequest(resultado.Error);
            return CreatedAtAction(nameof(GetById),
                new { id = resultado.Reserva!.Id },
                resultado.Reserva);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Editar(Guid id, [FromBody] EditarReservaDto dto)
        {
            var resultado = await _reservaService.EditarAsync(id, dto);
            if (!resultado.Ok)
                return BadRequest(resultado.Error);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Cancelar(Guid id)
        {
            var resultado = await _reservaService.CancelarAsync(id);
            if (!resultado.Ok)
                return BadRequest(resultado.Error);
            return NoContent();
        }
    }
}