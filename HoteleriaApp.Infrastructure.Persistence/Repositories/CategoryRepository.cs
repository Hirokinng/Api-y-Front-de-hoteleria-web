using HoteleriaApp.Core.Application.DTOs.Category;
using HoteleriaApp.Core.Application.Interfaces;
using HoteleriaApp.Core.Domain.Entities;
using HoteleriaApp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HoteleriaApp.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            return await _context.Categories
                .Where(x => x.IsActive)
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PricePerNight = x.PricePerNight,
                    Characteristics = x.Characteristics,
                    IsActive = x.IsActive
                }).ToListAsync();
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                PricePerNight = category.PricePerNight,
                Characteristics = category.Characteristics,
                IsActive = category.IsActive
            };
        }

        public async Task CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                PricePerNight = dto.PricePerNight,
                Characteristics = dto.Characteristics,
                IsActive = true
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(dto.Id);
            if (category == null) return;

            category.Name = dto.Name;
            category.Description = dto.Description;
            category.PricePerNight = dto.PricePerNight;
            category.Characteristics = dto.Characteristics;
            category.IsActive = dto.IsActive;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return;

            category.IsActive = false;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
