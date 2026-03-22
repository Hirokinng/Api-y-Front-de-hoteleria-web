using System.ComponentModel.DataAnnotations;

namespace HoteleriaApp.Core.Domain.Entities
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; }

        public bool Activo { get; set; } = true;

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}