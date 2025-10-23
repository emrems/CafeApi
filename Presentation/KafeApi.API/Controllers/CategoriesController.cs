using KafeApi.Application.Dtos.CategoryDto;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriyServices _categoryServices;

        public CategoriesController(ICategoriyServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryServices.GetAllCategories();// burası bize responseDto döndürüyor
            if (!categories.Success)// ResponseDto'da Success false ise, yani hata varsa
            {
                if (categories.ErrorCode == ErrorCodes.NotFound)// 
                {
                    return Ok(categories);
                }
                return BadRequest();
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var categories = await _categoryServices.GetCategoryById(id);
            if (!categories.Success)
            {
                if (categories.ErrorCode == ErrorCodes.NotFound)
                {
                    return Ok(categories);

                }
                return BadRequest();
            }
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto dto)
        {
            var result =await _categoryServices.AddCategory(dto);
            if (!result.Success)
            {
                if (result.ErrorCode == ErrorCodes.ValidationError)
                {
                    return Ok(result); // Validation hatası varsa, yani dto'da bir sorun varsa, bu durumda 200 OK ile birlikte hata mesajı döndürülür
                }
                return BadRequest();
               
            }
            return Ok(result);    
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto dto)
        {
            var result = await _categoryServices.UpdateCategory(dto);
            if (!result.Success)
            {
                if (result.ErrorCode is ErrorCodes.NotFound or ErrorCodes.ValidationError)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryServices.DeleteCategory(id);
            if (!result.Success)
            {
                if (result.ErrorCode == ErrorCodes.NotFound)
                {
                    return NotFound(result.Message);
                }
                return BadRequest(result.Message);

               
            }
            return NoContent(); 
        }
    }
}