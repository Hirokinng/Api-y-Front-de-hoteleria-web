using System.Security.Claims;
using HoteleriaApp.Core.Application.DTOs;
using HoteleriaApp.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoteleriaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteServicio _servicio;

        public ClientesController(IClienteServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(new { ok = true, message = "ClientesController OK" });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] ClienteRegisterDto dto)
        {
            var result = _servicio.Registrar(dto);
            if (!result.ok) return BadRequest(result);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] ClienteLoginDto dto)
        {
            var result = _servicio.Login(dto);
            if (!result.ok) return BadRequest(result);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("actualizar-perfil")]
        public IActionResult ActualizarPerfil([FromBody] ClienteUpdateDto dto)
        {
            var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(claimId, out var clienteId))
                return Unauthorized(new { ok = false, message = "Token inválido." });

            var result = _servicio.ActualizarPerfil(clienteId, dto);
            if (!result.ok) return BadRequest(result);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("cambiar-password")]
        public IActionResult CambiarPassword([FromBody] ClienteCambiarPasswordDto dto)
        {
            var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(claimId, out var clienteId))
                return Unauthorized(new { ok = false, message = "Token inválido." });

            var result = _servicio.CambiarPassword(clienteId, dto);
            if (!result.ok) return BadRequest(result);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("perfil")]
        public IActionResult Perfil()
        {
            var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(claimId, out var clienteId))
                return Unauthorized(new { ok = false, message = "Token inválido." });

            var perfil = _servicio.ObtenerPerfil(clienteId);

            if (perfil == null)
                return NotFound(new { ok = false, message = "Perfil no encontrado." });

            return Ok(perfil);
        }
    }
}