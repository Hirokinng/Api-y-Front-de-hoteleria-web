using HoteleriaApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteleriaApp.Core.Application.Interfaces 
{
    public interface IUsuarioRepositorio
    {
        Usuario? GetUsuarioPorEmail(string email);
    }
}
