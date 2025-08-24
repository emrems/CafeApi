using KafeApi.Application.Dtos.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Abstract
{
    public interface ICategoriyServices
    {
        Task<List<ResultCategoryDto>> GetAllCategories();
        Task<DetailCategoryDto> GetCategoryById(int id);
        Task AddCategory(CreateCategoryDto dto);
        Task UpdateCategory(UpdateCategoryDto dto);
        Task DeleteCategory(int id);
    }
}
