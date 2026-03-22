namespace HoteleriaApp.Core.Domain.Entities
{
    public class HabitacionAmenity
    {
        public int HabitacionId { get; set; }
        public Habitacion Habitacion { get; set; } = null!;

        public int AmenityId { get; set; }
        public Amenity Amenity { get; set; } = null!;
    }
}
