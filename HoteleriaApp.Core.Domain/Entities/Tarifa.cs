using System;

namespace HoteleriaApp.Core.Domain.Entities
{
    public class Tarifa : BaseEntity
    {
        public int IdCategoria { get; set; }
        public int? IdTemporada { get; set; }
        public decimal PrecioNoche { get; set; }
        public bool Activo { get; set; } = true;

        public Categoria Categoria { get; set; } = null!;
        public Temporada? Temporada { get; set; }
    }
}