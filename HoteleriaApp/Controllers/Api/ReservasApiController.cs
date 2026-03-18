using HoteleriaApp.Core.Application.DTOs.Reservas;
using HoteleriaApp.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HoteleriaApp.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaApiController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservaApiController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        // GET: api/reservaapi
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ReservaDto>>> GetAll()
        {
            var reservas = await _reservaService.GetAllAsync();
            return Ok(reservas);
        }

        // GET: api/reservaapi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaDto>> GetById(int id)
        {
            var reserva = await _reservaService.GetByIdAsync(id);

            if (reserva == null)
                return NotFound();

            return Ok(reserva);
        }

        // GET: api/reservaapi/disponibilidad
        [HttpGet("disponibilidad")]
        public async Task<ActionResult<IReadOnlyList<HabitacionDisponibleDto>>> BuscarDisponibilidad(
            [FromQuery] int idCategoria,
            [FromQuery] DateOnly fechaEntrada,
            [FromQuery] DateOnly fechaSalida,
            [FromQuery] byte numHuespedes)
        {
            var disponibles = await _reservaService.BuscarDisponibilidadAsync(
                idCategoria, fechaEntrada, fechaSalida, numHuespedes);

            return Ok(disponibles);
        }

        // POST: api/reservaapi
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearReservaDto dto)
        {
            var resultado = await _reservaService.CrearAsync(dto);

            if (!resultado.Ok)
                return BadRequest(resultado.Error);

            return CreatedAtAction(nameof(GetById),
                new { id = resultado.Reserva!.Id },
                resultado.Reserva);
        }

        // PUT: api/reservaapi
        [HttpPut]
        public async Task<ActionResult> Editar([FromBody] EditarReservaDto dto)
        {
            var resultado = await _reservaService.EditarAsync(dto);

            if (!resultado.Ok)
                return BadRequest(resultado.Error);

            return NoContent();
        }

        // DELETE: api/reservaapi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Cancelar(int id)
        {
            var resultado = await _reservaService.CancelarAsync(id);

            if (!resultado.Ok)
                return BadRequest(resultado.Error);

            return NoContent();
        }
    }
}