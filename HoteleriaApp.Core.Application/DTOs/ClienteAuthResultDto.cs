using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteleriaApp.Core.Application.DTOs;

public class ClienteAuthResultDto
{
    public bool ok { get; set; }
    public string message { get; set; } = string.Empty;
    public Guid? id_cliente { get; set; }
    public string? email { get; set; }
    public string? token { get; set; }
}
