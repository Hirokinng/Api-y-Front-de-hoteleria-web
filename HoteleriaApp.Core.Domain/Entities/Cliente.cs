using System.ComponentModel.DataAnnotations;

namespace HoteleriaApp.Core.Domain.Entities;

public class Cliente
{
    [Key]
    public int id_cliente { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string? telefono { get; set; }
    public string password_hash { get; set; } = string.Empty;
    public DateTime fecha_registro { get; set; }
    public bool activo { get; set; } = true;
}