using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteleriaApp.Core.Application.DTOs
{
    public class ClientePerfilDto
    {
        public Guid id_cliente { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string? telefono { get; set; }
        public bool activo { get; set; }
        public DateTime fecha_registro { get; set; }
    }
}
