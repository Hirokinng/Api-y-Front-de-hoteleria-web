namespace HoteleriaApp.Core.Domain.Entities
{
    public class ReservaHabitacion
    {
        public int Id { get; set; }

        public int ReservaId { get; set; }
        public Reserva Reserva { get; set; } = null!;

        public int HabitacionId { get; set; }
        public Habitacion Habitacion { get; set; } = null!;

        public bool EsPrincipal { get; set; }
    }
}
