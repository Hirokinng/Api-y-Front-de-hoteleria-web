using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteleriaApp.Core.Application.DTOs
{
    public class ClienteUpdateDto
    {
        public int id_cliente { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string? telefono { get; set; }
    }
}