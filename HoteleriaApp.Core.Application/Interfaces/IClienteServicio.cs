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
        ClienteAuthResultDto ActualizarPerfil(ClienteUpdateDto dto);
        ClienteAuthResultDto ObtenerPerfil(int idCliente);
    }
}