
namespace HoteleriaApp.Core.Domain.Entities
{
    public class ReservaHabitacion
    {
        public Guid Id { get; set; }              
        public Guid ReservaId { get; set; }       
        public Reserva Reserva { get; set; } = null!;
        public Guid HabitacionId { get; set; }    
        public Habitacion Habitacion { get; set; } = null!;
        public bool EsPrincipal { get; set; }
    }

}