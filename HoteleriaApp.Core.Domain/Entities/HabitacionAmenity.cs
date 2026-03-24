namespace HoteleriaApp.Core.Domain.Entities
{
    public class HabitacionAmenity
    {
        public Guid Id { get; set; }
        public Guid HabitacionId { get; set; }
        public Habitacion Habitacion { get; set; } = null!;

        public Guid AmenityId { get; set; }
        public Amenity Amenity { get; set; } = null!;
    }
}
