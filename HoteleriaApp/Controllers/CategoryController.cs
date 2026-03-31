using HoteleriaApp.Core.Application.DTOs.Category;
using HoteleriaApp.Core.Application.Interfaces;
<<<<<<< Updated upstream
=======
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
>>>>>>> Stashed changes
using Microsoft.AspNetCore.Mvc;

namespace HoteleriaApp.Controllers
{
<<<<<<< Updated upstream
=======
    [Authorize(Roles = "Admin", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
>>>>>>> Stashed changes
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _categoryService.CreateAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();

            var updateDto = new UpdateCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                PricePerNight = category.PricePerNight,
                Characteristics = category.Characteristics,
                IsActive = category.IsActive
            };

            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _categoryService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
