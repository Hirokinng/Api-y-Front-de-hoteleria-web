using System;

public class ReservaDto
{
    public int Id { get; set; }
    public string NumeroReserva { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateOnly FechaEntrada { get; set; }
    public DateOnly FechaSalida { get; set; }
    public byte NumeroHuespedes { get; set; }
    public short TotalNoches { get; set; }
    public decimal PrecioBaseNoche { get; set; }
    public decimal SubtotalHabitacion { get; set; }
    public decimal TotalServicios { get; set; }
    public decimal Total { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public string NombreCategoria { get; set; } = string.Empty;
    public string NumeroHabitacion { get; set; } = string.Empty;
    public List<string> Servicios { get; set; } = [];
}
