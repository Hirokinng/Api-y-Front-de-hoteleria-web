using System;

public class HistorialEstadoReserva : BaseEntity
{
    public int IdReserva { get; set; }
    public EstadoReserva? EstadoAnterior { get; set; }
    public EstadoReserva EstadoNuevo { get; set; }
    public int? IdUsuario { get; set; }
    public DateTime FechaCambio { get; set; } = DateTime.UtcNow;
    public string? Observacion { get; set; }

    public Reserva Reserva { get; set; } = null!;
    public Usuario? Usuario { get; set; }
}
