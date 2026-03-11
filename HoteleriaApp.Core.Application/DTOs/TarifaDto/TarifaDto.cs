using System;

public class TarifaDto
{
    public int Id { get; set; }
    public string NombreCategoria { get; set; } = string.Empty;
    public string Temporada { get; set; } = "Base";
    public decimal PrecioNoche { get; set; }
}
