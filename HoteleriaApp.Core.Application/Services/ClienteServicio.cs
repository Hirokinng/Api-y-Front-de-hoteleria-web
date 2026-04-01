using System;
using HoteleriaApp.Core.Application.DTOs;
using HoteleriaApp.Core.Application.Interfaces;
using HoteleriaApp.Core.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace HoteleriaApp.Core.Application.Services
{
    public class ClienteServicio : IClienteServicio
    {
        private readonly IClienteRepositorio _repo;
<<<<<<< HEAD
        private readonly IUsuarioRepositorio _usuarioRepo; // <-- 1. Agregamos el nuevo repo
        private readonly IEmailServicio _email;
        private readonly IConfiguration _configuration;

        // 2. Lo inyectamos en el constructor
=======
        private readonly IUsuarioRepositorio _usuarioRepo;
        private readonly IEmailServicio _email;
        private readonly IConfiguration _configuration;

>>>>>>> feature/categorias
        public ClienteServicio(IClienteRepositorio repo, IUsuarioRepositorio usuarioRepo, IEmailServicio email, IConfiguration configuration)
        {
            _repo = repo;
            _usuarioRepo = usuarioRepo;
            _email = email;
            _configuration = configuration;
        }

        public ClienteAuthResultDto Registrar(ClienteRegisterDto dto)
        {
            var nombre = (dto.nombre ?? "").Trim();
            var email = (dto.email ?? "").Trim().ToLower();
            var password = dto.password ?? "";

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return new ClienteAuthResultDto { ok = false, message = "Nombre, email y password son obligatorios." };

            var existe = _repo.GetClientePorEmail(email);
            if (existe != null)
                return new ClienteAuthResultDto { ok = false, message = "Ese email ya está registrado." };

            
            var hash = BCrypt.Net.BCrypt.HashPassword(password);

            var cliente = new Cliente
            {
                Nombre = nombre,
                Email = email,
                Telefono = string.IsNullOrWhiteSpace(dto.telefono) ? null : dto.telefono.Trim(),
                PasswordHash = hash,
                FechaRegistro = DateTime.UtcNow,
                Activo = true
            };

            _repo.Crear(cliente);
            _repo.Guardar();

<<<<<<< HEAD
            // Notificación (por ahora tu EmailServicio lo imprime en consola; luego se pone SMTP real)
=======
>>>>>>> feature/categorias
            _email.Enviar(cliente.Email, "Bienvenido a SGHR", $"Hola {cliente.Nombre}, tu registro fue exitoso.");

            return new ClienteAuthResultDto
            {
                ok = true,
                message = "Cliente registrado.",
                id_cliente = cliente.Id,
                email = cliente.Email
            };
        }

        public ClienteAuthResultDto Login(ClienteLoginDto dto)
        {
            var email = (dto.email ?? "").Trim().ToLower();
            var password = dto.password ?? "";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return new ClienteAuthResultDto { ok = false, message = "Email y password son obligatorios." };

            Guid idLogueado = Guid.Empty;
            string nombreLogueado = "";
            string emailLogueado = "";
            string rolAsignado = "";
            bool loginExitoso = false;

<<<<<<< HEAD
            // --- PASO 1: ¿ES UN ADMINISTRADOR/EMPLEADO? ---
=======
>>>>>>> feature/categorias
            var admin = _usuarioRepo.GetUsuarioPorEmail(email);
            if (admin != null && admin.Activo)
            {
                bool okPass = (!string.IsNullOrWhiteSpace(admin.PasswordHash) && admin.PasswordHash.StartsWith("$2"))
                    ? BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash)
                    : (admin.PasswordHash == password);

                if (okPass)
                {
                    idLogueado = admin.Id;
                    nombreLogueado = admin.Nombre;
                    emailLogueado = admin.Email;
<<<<<<< HEAD
                    rolAsignado = "Admin"; // <-- ¡El gafete VIP!
=======
                    rolAsignado = "Admin";
>>>>>>> feature/categorias
                    loginExitoso = true;
                }
            }

<<<<<<< HEAD
            // --- PASO 2: SI NO ES ADMIN, ¿ES UN CLIENTE NORMAL? ---
=======
>>>>>>> feature/categorias
            if (!loginExitoso)
            {
                var cliente = _repo.GetClientePorEmail(email);
                if (cliente != null && cliente.Activo)
                {
                    bool okPass = (!string.IsNullOrWhiteSpace(cliente.PasswordHash) && cliente.PasswordHash.StartsWith("$2"))
                        ? BCrypt.Net.BCrypt.Verify(password, cliente.PasswordHash)
                        : (cliente.PasswordHash == password);

                    if (okPass)
                    {
                        idLogueado = cliente.Id;
                        nombreLogueado = cliente.Nombre;
                        emailLogueado = cliente.Email;
<<<<<<< HEAD
                        rolAsignado = "Cliente"; // <-- Gafete de huésped
=======
                        rolAsignado = "Cliente";
>>>>>>> feature/categorias
                        loginExitoso = true;
                    }
                }
            }

<<<<<<< HEAD
            // --- PASO 3: SI NO ESTÁ EN NINGUNA TABLA ---
            if (!loginExitoso)
                return new ClienteAuthResultDto { ok = false, message = "Credenciales inválidas." };

            // --- PASO 4: GENERAR EL JWT ---
=======
            if (!loginExitoso)
                return new ClienteAuthResultDto { ok = false, message = "Credenciales inválidas." };

>>>>>>> feature/categorias
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(key))
                return new ClienteAuthResultDto { ok = false, message = "JWT Key no configurada." };

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, idLogueado.ToString()),
        new Claim(ClaimTypes.Name, nombreLogueado),
        new Claim(ClaimTypes.Email, emailLogueado),
<<<<<<< HEAD
        new Claim(ClaimTypes.Role, rolAsignado) // <-- ESTO ACTIVA EL [Authorize(Roles="Admin")] EN TU API
=======
        new Claim(ClaimTypes.Role, rolAsignado)
>>>>>>> feature/categorias
    };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return new ClienteAuthResultDto
            {
                ok = true,
                message = "Login correcto.",
                id_cliente = idLogueado,
                email = emailLogueado,
                token = token,
                rol = rolAsignado 
            };


        }

        public ClienteAuthResultDto ActualizarPerfil(Guid clienteId, ClienteUpdateDto dto)
        {
            if (clienteId == Guid.Empty)
                return new ClienteAuthResultDto { ok = false, message = "Cliente inválido." };

            var nombre = (dto.nombre ?? "").Trim();
            var telefono = dto.telefono?.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
                return new ClienteAuthResultDto { ok = false, message = "El nombre es obligatorio." };

            var cliente = _repo.GetClientePorId(clienteId);

            if (cliente == null || !cliente.Activo)
                return new ClienteAuthResultDto { ok = false, message = "Cliente no encontrado o inactivo." };

            cliente.Nombre = nombre;
            cliente.Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono;

            _repo.Guardar();

            _email.Enviar(cliente.Email, "Perfil actualizado", $"Hola {cliente.Nombre}, tus datos fueron actualizados.");

            return new ClienteAuthResultDto
            {
                ok = true,
                message = "Perfil actualizado.",
                id_cliente = cliente.Id,
                email = cliente.Email
            };
        }

        public ClienteAuthResultDto CambiarPassword(Guid clienteId, ClienteCambiarPasswordDto dto)
        {
            if (clienteId == Guid.Empty)
                return new ClienteAuthResultDto { ok = false, message = "Cliente inválido." };

            var passwordActual = dto.passwordActual ?? "";
            var passwordNueva = dto.passwordNueva ?? "";

            if (string.IsNullOrWhiteSpace(passwordActual) || string.IsNullOrWhiteSpace(passwordNueva))
                return new ClienteAuthResultDto { ok = false, message = "La contraseña actual y la nueva son obligatorias." };

            if (passwordNueva.Length < 6)
                return new ClienteAuthResultDto { ok = false, message = "La nueva contraseña debe tener al menos 6 caracteres." };

            var cliente = _repo.GetClientePorId(clienteId);

            if (cliente == null || !cliente.Activo)
                return new ClienteAuthResultDto { ok = false, message = "Cliente no encontrado o inactivo." };

            bool okPass;
            if (!string.IsNullOrWhiteSpace(cliente.PasswordHash) && cliente.PasswordHash.StartsWith("$2"))
                okPass = BCrypt.Net.BCrypt.Verify(passwordActual, cliente.PasswordHash);
            else
                okPass = (cliente.PasswordHash == passwordActual);

            if (!okPass)
                return new ClienteAuthResultDto { ok = false, message = "La contraseña actual no es correcta." };

            cliente.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordNueva);

            _repo.Guardar();

            _email.Enviar(cliente.Email, "Contraseña actualizada", $"Hola {cliente.Nombre}, tu contraseña fue cambiada correctamente.");

            return new ClienteAuthResultDto
            {
                ok = true,
                message = "Contraseña actualizada correctamente.",
                id_cliente = cliente.Id,
                email = cliente.Email
            };
        }

        public ClientePerfilDto? ObtenerPerfil(Guid clienteId)
        {
            if (clienteId == Guid.Empty)
                return null;

            var cliente = _repo.GetClientePorId(clienteId);

            if (cliente == null || !cliente.Activo)
                return null;

            return new ClientePerfilDto
            {
                id_cliente = cliente.Id,
                nombre = cliente.Nombre,
                email = cliente.Email,
                telefono = cliente.Telefono,
                activo = cliente.Activo,
                fecha_registro = cliente.FechaRegistro
            };
        }

    }
}