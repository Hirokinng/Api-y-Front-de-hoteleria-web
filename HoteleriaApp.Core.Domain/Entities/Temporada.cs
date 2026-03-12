using System;

public class Temporada : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFin { get; set; }
    public bool Activo { get; set; } = true;

    public ICollection<Tarifa> Tarifas { get; set; } = [];
}
