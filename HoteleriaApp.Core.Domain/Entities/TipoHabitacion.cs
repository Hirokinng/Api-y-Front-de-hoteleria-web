namespace HoteleriaApp.Core.Domain.Entities
{
    public class TipoHabitacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int CapacidadBase { get; set; }
        public decimal PrecioBasePorNoche { get; set; }

        public ICollection<Habitacion> Habitaciones { get; set; } = new List<Habitacion>();
    }
}
