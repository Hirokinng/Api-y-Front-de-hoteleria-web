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
        private Piso() { }

        public Piso(string nombre, string descripcion, int numeroPiso)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Descripcion = descripcion;
            NumeroPiso = numeroPiso;
        }

        public void Actualizar(string nombre, string descripcion, int numeroPiso)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            NumeroPiso = numeroPiso;
        }

        public void Update(string nombre, string descripcion, int numeroPiso)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío");

            Nombre = nombre;
            Descripcion = descripcion ?? string.Empty;
            NumeroPiso = numeroPiso;
        }
    }
}