using KafeApi.Application.Dtos.CategoryDto;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoriyServices _categoryServices;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoriyServices categoryServices, ILogger<CategoriesController> logger)
        {
            _categoryServices = categoryServices;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            _logger.LogInformation("GetAllCategories endpoint called.");// zaten global olarak loglama yapılıyor. Burdaki ise özel loglama için
            var categories = await _categoryServices.GetAllCategories();// burası bize responseDto döndürüyor
            _logger.LogInformation("GetAllCategories endpoint finished."+ categories);
            return CreateResponse(categories);
        }

        [HttpGet("getCategoriesWithMenuItem")]
        public async Task<IActionResult> GetCategoriesWithMenuItem()
        {
            var categories = await _categoryServices.GetAllCategoriesWithMenuItems();
            return CreateResponse(categories);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var categories = await _categoryServices.GetCategoryById(id);
            return CreateResponse(categories);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto dto)
        {
            var result =await _categoryServices.AddCategory(dto);
            return CreateResponse(result);    
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto dto)
        {
            var result = await _categoryServices.UpdateCategory(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryServices.DeleteCategory(id);
            return CreateResponse(result);
        }
    }
}