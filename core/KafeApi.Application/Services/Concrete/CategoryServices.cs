using AutoMapper;
using KafeApi.Application.Dtos.CategoryDto;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Concrete
{
    public class CategoryServices : ICategoriyServices
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryServices(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task AddCategory(CreateCategoryDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Category DTO cannot be null");
            }

            // Map CreateCategoryDto to Category entity
            var category = _mapper.Map<Category>(dto);


            await _categoryRepository.AddAsync(category);
             
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"{id} li category bulunamadı");
            }
            await _categoryRepository.DeleteAsync(category);


        }

        public async Task<List<ResultCategoryDto>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                if (categories == null )
                {
                    throw new InvalidOperationException("No categories found.");
                }
                // category mapplenecek
                var ResultCategoryDtos = _mapper.Map<List<ResultCategoryDto>>(categories);
                return ResultCategoryDtos;


            } catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving categories", ex);

            }
        }
        public async Task<DetailCategoryDto> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"{id} li category bulunamadı");
            }
            // category mapplenecek  
            var detailCategoryDto = _mapper.Map<DetailCategoryDto>(category);
            return detailCategoryDto;
        }

        public async Task UpdateCategory(UpdateCategoryDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "category null olamaz");

            var category = await _categoryRepository.GetByIdAsync(dto.Id);
            if (category == null)
                throw new KeyNotFoundException($"{dto.Id} li category bulunamadı");

            // DTO -> mevcut Category
            _mapper.Map(dto, category);

            await _categoryRepository.UpdateAsync(category);
        }

    }
}
