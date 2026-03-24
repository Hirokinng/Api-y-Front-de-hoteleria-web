using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoteleriaApp.Core.Domain.Entities;

namespace HoteleriaApp.Core.Application.Interfaces
{
    public interface IClienteRepositorio
    {
        void Crear(Cliente cliente);
        IEnumerable<Cliente> GetClientes();
        Cliente? GetClientePorId(Guid id);
        Cliente? GetClientePorEmail(string email);
        void Guardar();

    }
}