using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using ServerApp.Repositories;
using ServerApp.ViewModels;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("get-all-categories")]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var categoriesVm = new List<CategoryVm>();

            foreach (var category in categories)
            {
                categoriesVm.Add(new CategoryVm()
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return Ok(categoriesVm);
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryVm categoryVm)
        {
            var category = new Category()
            {
                Name = categoryVm.Name
            };

            _categoryRepository.Add(category);

            return Ok(category);
        }
    }
}
