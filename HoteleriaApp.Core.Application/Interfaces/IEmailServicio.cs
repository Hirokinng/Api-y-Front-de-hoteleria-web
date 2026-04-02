using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteleriaApp.Core.Application.Interfaces
{
    public interface IEmailServicio
    {
        void Enviar(string para, string asunto, string mensaje);
    }
}
