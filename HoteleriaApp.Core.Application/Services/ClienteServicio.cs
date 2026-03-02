using System;
using HoteleriaApp.Core.Application.DTOs;
using HoteleriaApp.Core.Application.Interfaces;
using HoteleriaApp.Core.Domain.Entities;


namespace HoteleriaApp.Core.Application.Services
{
    public class ClienteServicio : IClienteServicio
    {
        private readonly IClienteRepositorio _repo;
        private readonly IEmailServicio _email;

        public ClienteServicio(IClienteRepositorio repo, IEmailServicio email)
        {
            _repo = repo;
            _email = email;
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
                nombre = nombre,
                email = email,
                telefono = string.IsNullOrWhiteSpace(dto.telefono) ? null : dto.telefono.Trim(),
                password_hash = hash,
                fecha_registro = DateTime.UtcNow,
                activo = true
            };

            _repo.Crear(cliente);
            _repo.Guardar();

            // Notificación (por ahora tu EmailServicio lo imprime en consola; luego se pone SMTP real)
            _email.Enviar(cliente.email, "Bienvenido a SGHR", $"Hola {cliente.nombre}, tu registro fue exitoso.");

            return new ClienteAuthResultDto
            {
                ok = true,
                message = "Cliente registrado.",
                id_cliente = cliente.id_cliente,
                email = cliente.email
            };
        }

        public ClienteAuthResultDto Login(ClienteLoginDto dto)
        {
            var email = (dto.email ?? "").Trim().ToLower();
            var password = dto.password ?? "";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return new ClienteAuthResultDto { ok = false, message = "Email y password son obligatorios." };

            var cliente = _repo.GetClientePorEmail(email);

            if (cliente == null || !cliente.activo)
                return new ClienteAuthResultDto { ok = false, message = "Credenciales inválidas." };

            // Compatibilidad: si es BCrypt, verificamos con Verify.
            // Si NO parece BCrypt (data vieja), comparamos texto plano para que no se rompa.
            bool okPass;
            if (!string.IsNullOrWhiteSpace(cliente.password_hash) && cliente.password_hash.StartsWith("$2"))
                okPass = BCrypt.Net.BCrypt.Verify(password, cliente.password_hash);
            else
                okPass = (cliente.password_hash == password);

            if (!okPass)
                return new ClienteAuthResultDto { ok = false, message = "Credenciales inválidas." };

            return new ClienteAuthResultDto
            {
                ok = true,
                message = "Login correcto.",
                id_cliente = cliente.id_cliente,
                email = cliente.email
            };
        }

        public ClienteAuthResultDto ActualizarPerfil(ClienteUpdateDto dto)
        {
            if (dto.id_cliente <= 0)
                return new ClienteAuthResultDto { ok = false, message = "id_cliente inválido." };

            var nombre = (dto.nombre ?? "").Trim();
            var telefono = dto.telefono?.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
                return new ClienteAuthResultDto { ok = false, message = "El nombre es obligatorio." };

            var cliente = _repo.GetClientePorId(dto.id_cliente);

            if (cliente == null || !cliente.activo)
                return new ClienteAuthResultDto { ok = false, message = "Cliente no encontrado o inactivo." };

            cliente.nombre = nombre;
            cliente.telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono;

            _repo.Guardar();

            _email.Enviar(cliente.email, "Perfil actualizado", $"Hola {cliente.nombre}, tus datos fueron actualizados.");

            return new ClienteAuthResultDto
            {
                ok = true,
                message = "Perfil actualizado.",
                id_cliente = cliente.id_cliente,
                email = cliente.email
            };
        }
    }
}