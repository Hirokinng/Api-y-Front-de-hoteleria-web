using System;

public class Auditoria
{
    public long Id { get; set; }
    public int? IdUsuario { get; set; }
    public string Tabla { get; set; } = string.Empty;
    public string Operacion { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
}
