using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteleriaApp.Core.Domain.Entities
{
    public class Piso
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public string Descripcion { get; private set; } = string.Empty;
        public int NumeroPiso { get; private set; }
        public bool Activo { get; private set; }
        public string NombreClave { get; private set; } = string.Empty;
        private Piso() { }

        
        public Piso(string nombre, string descripcion, int numeroPiso, string nombreClave)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Descripcion = descripcion;
            NumeroPiso = numeroPiso;
            NombreClave = nombreClave;
        }

  
        public void Actualizar(string nombre, string descripcion, int numeroPiso, string nombreClave)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            NumeroPiso = numeroPiso;
            NombreClave = nombreClave;
        }

        public void Update(string nombre, string descripcion, int numeroPiso, string nombreClave)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío");

            Nombre = nombre;
            Descripcion = descripcion ?? string.Empty;
            NumeroPiso = numeroPiso;
            NombreClave = nombreClave ?? string.Empty;
        }
    }
}