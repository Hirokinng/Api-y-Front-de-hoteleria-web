using System;

public class Pago : BaseEntity
{
    public int IdReserva { get; set; }
    public decimal Monto { get; set; }
    public EstadoPago Estado { get; set; } = EstadoPago.Pendiente;
    public string? ReferenciaExterna { get; set; }
    public DateTime? FechaPago { get; set; }

    public Reserva Reserva { get; set; } = null!;
}
