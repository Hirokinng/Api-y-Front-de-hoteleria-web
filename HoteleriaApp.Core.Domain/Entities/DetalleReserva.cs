using System;

namespace HoteleriaApp.Core.Domain.Entities
{
    public class DetalleReserva : BaseEntity
    {
        public Guid IdReserva { get; set; }
        public Guid IdHabitacion { get; set; }

        public Reserva Reserva { get; set; } = null!;
        public Habitacion Habitacion { get; set; } = null!;
    }

}