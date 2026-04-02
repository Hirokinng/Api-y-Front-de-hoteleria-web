using System;

namespace HoteleriaApp.Core.Domain.Entities
{
    public class Servicio : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;

        public ICollection<CategoriaServicio> CategoriaServicios { get; set; } = [];
        public ICollection<ReservaServicio> ReservaServicios { get; set; } = [];
    }

}