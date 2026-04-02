using System;

namespace HoteleriaApp.Core.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public RolUsuario Rol { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public ICollection<Reserva> Reservas { get; set; } = [];
        public ICollection<HistorialEstadoReserva> Historiales { get; set; } = [];
    }

}