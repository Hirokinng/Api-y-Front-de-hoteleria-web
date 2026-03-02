using HoteleriaApp.Core.Application.DTOs;
using HoteleriaApp.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HoteleriaApp.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteServicio _servicio;

        public ClientesController(IClienteServicio servicio)
        {
            _servicio = servicio;
        }

        // GET: /Clientes/Test
        [HttpGet]
        public IActionResult Test()
        {
            return Json(new { ok = true, message = "ClientesController OK" });
        }

        // POST: /Clientes/Register
        [HttpPost]
        public IActionResult Register([FromBody] ClienteRegisterDto dto)
        {
            var result = _servicio.Registrar(dto);
            if (!result.ok) return BadRequest(result);
            return Ok(result);
        }

        // POST: /Clientes/Login
        [HttpPost]
        public IActionResult Login([FromBody] ClienteLoginDto dto)
        {
            var result = _servicio.Login(dto);
            if (!result.ok) return BadRequest(result);
            return Ok(result);
        }

        // POST: /Clientes/ActualizarPerfil
        [HttpPost]
        public IActionResult ActualizarPerfil([FromBody] ClienteUpdateDto dto)
        {
            var result = _servicio.ActualizarPerfil(dto);
            if (!result.ok) return BadRequest(result);
            return Ok(result);
        }
    }
}
