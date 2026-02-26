
namespace HoteleriaApp.Core.Application.DTOs.Category
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public string Characteristics { get; set; }
    }
}
