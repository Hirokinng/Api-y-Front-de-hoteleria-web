namespace HoteleriaApp.Core.Domain.Entities
{
    public class Reserva
    {
        public int Id { get; set; }
        public string NumeroReserva { get; set; } = null!;
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public int? IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
        public int IdCategoria { get; set; }
        public Category Category { get; set; } = null!;
        public DateOnly FechaEntrada { get; set; }
        public DateOnly FechaSalida { get; set; }
        public int NumeroHuespedes { get; set; }
        public EstadoReserva Estado { get; set; } = EstadoReserva.Confirmada;
        public decimal PrecioBaseNoche { get; set; }
        public int TotalNoches { get; set; }
        public decimal SubtotalHabitacion { get; set; }
        public decimal TotalServicios { get; set; } = 0m;
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaCancelacion { get; set; }
        public string? ReferenciaExterna { get; set; }
        public ICollection<DetalleReserva> DetallesReserva { get; set; } = [];
        public ICollection<ReservaServicio> ReservaServicios { get; set; } = [];
        public ICollection<Pago> Pagos { get; set; } = [];
        public ICollection<HistorialEstadoReserva> Historial { get; set; } = [];
        public ICollection<ReservaHabitacion> Habitaciones { get; set; } = new List<ReservaHabitacion>();
       
    }
}