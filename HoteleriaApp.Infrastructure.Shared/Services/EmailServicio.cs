using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoteleriaApp.Core.Application.Interfaces;

namespace HoteleriaApp.Infrastructure.Shared.Services
{
    public class EmailServicio : IEmailServicio
    {
        public void Enviar(string para, string asunto, string mensaje)
        {
            Console.WriteLine("=== EMAIL SIMULADO ===");
            Console.WriteLine($"Para: {para}");
            Console.WriteLine($"Asunto: {asunto}");
            Console.WriteLine($"Mensaje: {mensaje}");
            Console.WriteLine("======================");
        }
    }
}