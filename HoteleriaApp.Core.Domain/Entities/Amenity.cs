namespace HoteleriaApp.Core.Domain.Entities
{
    public class Amenity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public ICollection<HabitacionAmenity> Habitaciones { get; set; } = new List<HabitacionAmenity>();
    }
}
