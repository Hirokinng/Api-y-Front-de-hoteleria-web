using System;

public class HabitacionDisponibleDto
{
    public int Id { get; set; }
    public string NumeroHabitacion { get; set; } = string.Empty;
    public string NombreCategoria { get; set; } = string.Empty;
    public byte CapacidadMax { get; set; }
    public int NumeroPiso { get; set; }
    public decimal PrecioNoche { get; set; }
}