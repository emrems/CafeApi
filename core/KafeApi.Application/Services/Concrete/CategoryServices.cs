using AutoMapper;
using FluentValidation;
using KafeApi.Application.Dtos.CategoryDto;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;

namespace KafeApi.Application.Services.Concrete
{
    public class CategoryServices : ICategoriyServices
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;

        public CategoryServices(IGenericRepository<Category> categoryRepository, IMapper mapper, IValidator<CreateCategoryDto> createCategoryValidator = null, IValidator<UpdateCategoryDto> updateCategoryValidator = null)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
        }

        public async Task<ResponseDto<object>> AddCategory(CreateCategoryDto dto)
        {
            try
            {
                // DTO validasyon
                var validationResult = await _createCategoryValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join("|", validationResult.Errors.Select(e => e.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError,
                       
                    };
                }
                var category = _mapper.Map<Category>(dto);
                await _categoryRepository.AddAsync(category);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Kategori eklendi",
                    
                };

            } catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }

        }

        public async Task<ResponseDto<object>> DeleteCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    new ResponseDto<object>()
                    {
                        Success = false,
                        Message = $"{id} li category bulunamadı",
                        ErrorCode = ErrorCodes.NotFound,
                        Data = null
                    };
                }
                await _categoryRepository.DeleteAsync(category);
                return new ResponseDto<object>()
                {
                    Success = true,
                    Message = $"{id} li category silindi",
                    Data = null
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<object>()
                {
                    Success = false,
                    Message = "bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception,
                    Data = null
                };


            }
        }

        public async Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                if (categories.Count == 0)
                {
                    return new ResponseDto<List<ResultCategoryDto>>()
                    {
                        Data = new List<ResultCategoryDto>(),
                        Success = false,
                        Message = "Kategori bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                // category mapplenecek
                var ResultCategoryDtos = _mapper.Map<List<ResultCategoryDto>>(categories);
                return new ResponseDto<List<ResultCategoryDto>> { Data = ResultCategoryDtos, Success = true };


            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultCategoryDto>>()
                {
                    Success = false,
                    Message = "bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception,
                    Data = null
                };

            }
        }
        public async Task<ResponseDto<DetailCategoryDto>> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return new ResponseDto<DetailCategoryDto>
                    {
                        Success = false,
                        Message = $"{id} li category bulunamadı",
                        ErrorCode = ErrorCodes.NotFound,
                        Data = null
                    };
                }
                // category mapplenecek  
                var detailCategoryDto = _mapper.Map<DetailCategoryDto>(category);
                return new ResponseDto<DetailCategoryDto>
                {
                    Success = true,
                    Data = detailCategoryDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailCategoryDto>
                {
                    Success = false,
                    Message = "bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception,
                    Data = null
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateCategory(UpdateCategoryDto dto)
        {
            try
            {
                // DTO validasyon
                var validationResult = await _updateCategoryValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join("|", validationResult.Errors.Select(e => e.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }

                var category = await _categoryRepository.GetByIdAsync(dto.Id);

                if (category == null)
                {
                    return new ResponseDto<object>()
                    {
                        Success = false,
                        Message = $"{dto.Id} li category bulunamadı",
                        ErrorCode = ErrorCodes.NotFound,
                        Data = null
                    };
                }

                // DTO -> mevcut Category
                _mapper.Map(dto, category);

                await _categoryRepository.UpdateAsync(category);
                return new ResponseDto<object>()
                {
                    Success = true,
                    Message = $"{dto.Id} li category güncellendi",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>()
                {
                    Success = false,
                    Message = "bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception,
                    Data = null
                };
            }
        }
    }
}
