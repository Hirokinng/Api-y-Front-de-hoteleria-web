using System;

namespace HoteleriaApp.Core.Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public DateOnly FechaRegistro { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public bool Activo { get; set; } = true;

        public ICollection<Reserva> Reservas { get; set; } = [];
    }

}
