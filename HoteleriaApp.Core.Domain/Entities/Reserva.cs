using System;

public class Reserva : BaseEntity
{
    public string NumeroReserva { get; set; } = string.Empty;
    public int IdCliente { get; set; }
    public int? IdUsuario { get; set; }
    public int IdCategoria { get; set; }
    public DateOnly FechaEntrada { get; set; }
    public DateOnly FechaSalida { get; set; }
    public byte NumeroHuespedes { get; set; }
    public EstadoReserva Estado { get; set; } = EstadoReserva.Confirmada;
    public decimal PrecioBaseNoche { get; set; }
    public short TotalNoches { get; set; }
    public decimal SubtotalHabitacion { get; set; }
    public decimal TotalServicios { get; set; }
    public decimal Total { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaCancelacion { get; set; }
    public string? ReferenciaExterna { get; set; }

    public Cliente Cliente { get; set; } = null!;
    public Usuario? Usuario { get; set; }
    public Categoria Categoria { get; set; } = null!;
    public ICollection<DetalleReserva> DetallesReserva { get; set; } = [];
    public ICollection<ReservaServicio> ReservaServicios { get; set; } = [];
    public ICollection<Pago> Pagos { get; set; } = [];
    public ICollection<HistorialEstadoReserva> Historiales { get; set; } = [];
}
