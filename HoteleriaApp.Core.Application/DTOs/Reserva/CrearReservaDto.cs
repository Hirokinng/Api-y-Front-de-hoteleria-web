using System;

public class CrearReservaDto
{
    public int IdCliente { get; set; }
    public int IdCategoria { get; set; }
    public int IdHabitacion { get; set; }
    public DateOnly FechaEntrada { get; set; }
    public DateOnly FechaSalida { get; set; }
    public byte NumeroHuespedes { get; set; }
    public int? IdUsuario { get; set; }
    public string? ReferenciaExterna { get; set; }
    public List<int> IdServicios { get; set; } = [];
}
