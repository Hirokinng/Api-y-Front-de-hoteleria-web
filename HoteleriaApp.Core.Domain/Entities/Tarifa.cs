using System;

namespace HoteleriaApp.Core.Domain.Entities
{
    public class Tarifa : BaseEntity
    {
        public Guid IdCategoria { get; set; }      
        public Guid? IdTemporada { get; set; }     
        public decimal PrecioNoche { get; set; }
        public bool Activo { get; set; } = true;
        public Category Categoria { get; set; } = null!;
        public Temporada? Temporada { get; set; }
    }
}