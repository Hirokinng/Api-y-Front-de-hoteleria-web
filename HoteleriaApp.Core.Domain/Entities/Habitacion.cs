using HoteleriaApp.Core.Domain.Enums;

namespace HoteleriaApp.Core.Domain.Entities
{
    public class Habitacion
    {
        public int Id { get; set; }
        public string Numero { get; set; } = null!;
        public int Piso { get; set; }

        public int TipoHabitacionId { get; set; }
        public TipoHabitacion TipoHabitacion { get; set; } = null!;

        public int Capacidad { get; set; }
        public HabitacionEstado Estado { get; set; } = HabitacionEstado.Disponible;

        public int IdCategoria { get; set; }
        public Category Categoria { get; set; } = null!;

        public ICollection<HabitacionAmenity> Amenities { get; set; } = new List<HabitacionAmenity>();
        public ICollection<BloqueoHabitacion> Bloqueos { get; set; } = new List<BloqueoHabitacion>();
        public ICollection<ReservaHabitacion> Reservas { get; set; } = new List<ReservaHabitacion>();
        public ICollection<DetalleReserva> DetallesReserva { get; set; } = new List<DetalleReserva>();
    }
}
