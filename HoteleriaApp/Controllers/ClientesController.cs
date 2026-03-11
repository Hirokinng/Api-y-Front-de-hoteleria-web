using HoteleriaApp.Core.Application.DTOs;
using HoteleriaApp.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HoteleriaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteServicio _servicio;
        private readonly IConfiguration _config;

        public ClientesController(IClienteServicio servicio, IConfiguration config)
        {
            _servicio = servicio;
            _config = config;
        }

        // GET: /api/Clientes/Test
        [AllowAnonymous]
        [HttpGet("Test")]
        public IActionResult Test() =>
            Ok(new { ok = true, message = "ClientesController OK" });

        // POST: /api/Clientes/Register
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] ClienteRegisterDto dto)
        {
            var result = _servicio.Registrar(dto);
            if (!result.ok) return BadRequest(result);

            result.token = GenerarToken(result.id_cliente!.Value, result.email!);
            return Ok(result);
        }

        // POST: /api/Clientes/Login
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] ClienteLoginDto dto)
        {
            var result = _servicio.Login(dto);
            if (!result.ok) return BadRequest(result);

            result.token = GenerarToken(result.id_cliente!.Value, result.email!);
            return Ok(result);
        }

        // GET: /api/Clientes/Perfil
        [Authorize]
        [HttpGet("Perfil")]
        public IActionResult Perfil()
        {
            // Lee el id directamente del token, el cliente no lo manda
            var idClaim = User.FindFirstValue("id_cliente");
            if (idClaim == null) return Unauthorized();

            var cliente = _servicio.ObtenerPerfil(int.Parse(idClaim));
            if (!cliente.ok) return NotFound(cliente);

            return Ok(cliente);
        }

        // PUT: /api/Clientes/ActualizarPerfil
        [Authorize]
        [HttpPut("ActualizarPerfil")]
        public IActionResult ActualizarPerfil([FromBody] ClienteUpdateDto dto)
        {
            
            var idClaim = User.FindFirstValue("id_cliente");
            if (idClaim == null) return Unauthorized();

            dto.id_cliente = int.Parse(idClaim);

            var result = _servicio.ActualizarPerfil(dto);
            if (!result.ok) return BadRequest(result);

            return Ok(result);
        }

        
        private string GenerarToken(int idCliente, string email)
        {
            var jwtKey = _config["Jwt:Key"]!;
            var jwtIssuer = _config["Jwt:Issuer"]!;
            var jwtAudience = _config["Jwt:Audience"]!;
            var expireMinutes = int.Parse(_config["Jwt:ExpireMinutes"] ?? "60");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,   idCliente.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("id_cliente",                  idCliente.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}