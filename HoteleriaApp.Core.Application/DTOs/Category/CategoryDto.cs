
namespace HoteleriaApp.Core.Application.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal PricePerNight { get; set; }
        public string? Characteristics { get; set; }
        public bool IsActive { get; set; }
    }
}
