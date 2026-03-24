using System;

namespace HoteleriaApp.Core.Domain.Entities
{
    public class HistorialEstadoReserva : BaseEntity
    {
        public Guid IdReserva { get; set; }
        public EstadoReserva? EstadoAnterior { get; set; }
        public EstadoReserva EstadoNuevo { get; set; }
        public Guid? IdUsuario { get; set; }
        public DateTime FechaCambio { get; set; } = DateTime.UtcNow;
        public string? Observacion { get; set; }

        public Reserva Reserva { get; set; } = null!;
        public Usuario? Usuario { get; set; }

    }

}