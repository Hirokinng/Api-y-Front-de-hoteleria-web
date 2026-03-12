namespace HoteleriaApp.Core.Domain.Entities
{
    public class Reserva
    {
        public int Id { get; set; }

        public string NombreHuesped { get; set; } = null!;
        public int CantidadHuespedes { get; set; }

        public DateOnly FechaCheckIn { get; set; }
        public DateOnly FechaCheckOut { get; set; }

        public ICollection<ReservaHabitacion> Habitaciones { get; set; } = new List<ReservaHabitacion>();
    }
}
