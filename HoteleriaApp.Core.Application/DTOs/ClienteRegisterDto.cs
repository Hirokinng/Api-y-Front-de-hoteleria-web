using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteleriaApp.Core.Application.DTOs;

public class ClienteRegisterDto
{
    public string nombre { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string? telefono { get; set; }
    public string password { get; set; } = string.Empty;
}
