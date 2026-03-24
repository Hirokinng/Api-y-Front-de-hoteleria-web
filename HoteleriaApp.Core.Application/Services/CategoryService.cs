using HoteleriaApp.Core.Application.DTOs.Category;
using HoteleriaApp.Core.Application.Interfaces;

namespace HoteleriaApp.Core.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<CategoryDto> GetByIdAsync(Guid id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(CreateCategoryDto dto)
        {
            await _categoryRepository.CreateAsync(dto);
        }

        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            await _categoryRepository.UpdateAsync(dto);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}
