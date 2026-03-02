using System;

public class Habitacion : BaseEntity
{
    public string NumeroHabitacion { get; set; } = string.Empty;
    public int IdPiso { get; set; }
    public int IdCategoria { get; set; }
    public EstadoHabitacion Estado { get; set; } = EstadoHabitacion.Disponible;
    public string? DescripcionAdicional { get; set; }
    public DateTime FechaUltimaActualizacion { get; set; } = DateTime.UtcNow;

    public Piso Piso { get; set; } = null!;
    public Categoria Categoria { get; set; } = null!;
    public ICollection<DetalleReserva> DetallesReserva { get; set; } = [];
}
