using KafeApi.Application.Dtos.CategoryDto;
using KafeApi.Application.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Abstract
{
    public interface ICategoriyServices
    {
        Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories();
        Task<ResponseDto<DetailCategoryDto>> GetCategoryById(int id);
        Task<ResponseDto<object>> AddCategory(CreateCategoryDto dto);
        Task<ResponseDto<object>> UpdateCategory(UpdateCategoryDto dto);
        Task<ResponseDto<object>> DeleteCategory(int id);
    }
}
