using System;

public class ActualizarReservaDto
{
    public int Id { get; set; }
    public DateOnly FechaEntrada { get; set; }
    public DateOnly FechaSalida { get; set; }
    public byte NumeroHuespedes { get; set; }
    public List<int> IdServicios { get; set; } = [];
}
