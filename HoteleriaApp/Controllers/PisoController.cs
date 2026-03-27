using HoteleriaApp.Core.Application.DTOs;
using HoteleriaApp.Core.Application.Services;
using HoteleriaApp.Core.Domain.Entities;
using HoteleriaApp.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoteleriaApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class PisosController : Controller
    {
        private readonly IPisoService _pisoService;

        public PisosController(IPisoService pisoService)
        {
            _pisoService = pisoService;
        }

 

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pisos = await _pisoService.GetAllAsync();
            return Ok(pisos);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id inválido");
            var piso = await _pisoService.GetByIdAsync(id);

            if (piso == null)
                return NotFound();

            return Ok(piso);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Piso piso)
        {

            await _pisoService.CreateAsync(piso);
            return Ok("Piso creado correctamente");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id inválido");
            await _pisoService.DeleteAsync(id);
            return Ok("Piso eliminado correctamente");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePisoDto dto)
        {
            try
            {
                var updated = await _pisoService.UpdateAsync(id, dto.Nombre, dto.Descripcion, dto.numeroPiso);

                if (!updated)
                    return NotFound();
                if (id == Guid.Empty)
                    return BadRequest("Id inválido");

                return Ok("Piso actualizado correctamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
