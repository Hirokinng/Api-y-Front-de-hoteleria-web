using System;

namespace HoteleriaApp.Core.Domain.Entities
{

    public class CategoriaServicio : BaseEntity
    {
        public Guid IdCategoria { get; set; }      
        public Guid IdServicio { get; set; }       
        public decimal Precio { get; set; }
        public bool Activo { get; set; } = true;
        public Category Categoria { get; set; } = null!;
        public Servicio Servicio { get; set; } = null!;
    }

}