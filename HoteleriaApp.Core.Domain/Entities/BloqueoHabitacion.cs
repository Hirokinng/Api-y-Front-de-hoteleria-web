using HoteleriaApp.Core.Domain.Enums;

namespace HoteleriaApp.Core.Domain.Entities
{
    public class BloqueoHabitacion
    {
        public Guid Id { get; set; }

        public Guid HabitacionId { get; set; }
        public Habitacion Habitacion { get; set; } = null!;

        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }

        public string Motivo { get; set; } = null!;
        public TipoBloqueoHabitacion TipoBloqueo { get; set; }
    }
}
