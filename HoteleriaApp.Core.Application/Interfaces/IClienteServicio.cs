using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoteleriaApp.Core.Application.DTOs;

namespace HoteleriaApp.Core.Application.Interfaces
{
    public interface IClienteServicio
    {
        ClienteAuthResultDto Registrar(ClienteRegisterDto dto);
        ClienteAuthResultDto Login(ClienteLoginDto dto);
        ClienteAuthResultDto ActualizarPerfil(Guid clienteId, ClienteUpdateDto dto);
        ClienteAuthResultDto CambiarPassword(Guid clienteId, ClienteCambiarPasswordDto dto);
        ClientePerfilDto? ObtenerPerfil(Guid clienteId);

    }
}