using System;

namespace HoteleriaApp.Core.Domain.Entities
{
    public class Categoria : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public byte CapacidadMax { get; set; }
        public bool Activo { get; set; } = true;

        public ICollection<Habitacion> Habitaciones { get; set; } = [];
        public ICollection<Tarifa> Tarifas { get; set; } = [];
        public ICollection<CategoriaServicio> CategoriaServicios { get; set; } = [];
        public ICollection<Reserva> Reservas { get; set; } = [];
    }

}