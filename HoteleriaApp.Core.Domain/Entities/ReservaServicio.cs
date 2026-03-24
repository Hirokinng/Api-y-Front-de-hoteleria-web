using System;

namespace HoteleriaApp.Core.Domain.Entities
{
    public class ReservaServicio : BaseEntity
    {
        public Guid IdReserva { get; set; }
        public Guid IdServicio { get; set; }
        public decimal PrecioAplicado { get; set; }

        public Reserva Reserva { get; set; } = null!;
        public Servicio Servicio { get; set; } = null!;

    }

}