using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteleriaApp.Core.Application.DTOs;

public class ClienteLoginDto
{
    public string email { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
}