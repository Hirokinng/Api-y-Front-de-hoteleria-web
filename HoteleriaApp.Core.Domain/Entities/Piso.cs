using System;

public class Piso : BaseEntity
{
    public short NumeroPiso { get; set; }
    public string? Descripcion { get; set; }
    public bool Activo { get; set; } = true;

    public ICollection<Habitacion> Habitaciones { get; set; } = [];
}
