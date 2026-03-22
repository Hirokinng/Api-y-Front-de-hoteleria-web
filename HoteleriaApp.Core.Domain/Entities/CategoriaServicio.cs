using System;

namespace HoteleriaApp.Core.Domain.Entities
{

    public class CategoriaServicio : BaseEntity
    {
        public int IdCategoria { get; set; }
        public int IdServicio { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; } = true;

        public Categoria Categoria { get; set; } = null!;
        public Servicio Servicio { get; set; } = null!;
    }

}