using AutoMapper;
using FluentValidation;
using KafeApi.Application.Dtos.MenuItemDto;
using KafeApi.Application.Dtos.ResponseDtos;
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
    public class MenuItemServices : IMenuItemServices
    {
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IValidator<CreateMenuItemDto> _addMenuItemValidator;
        private readonly IValidator<UpdateMenuItemDto> _updateMenuItemValidator;

        public MenuItemServices(IGenericRepository<MenuItem> menuItemRepository, IMapper mapper = null, IValidator<CreateMenuItemDto> addMenuItemValidator = null, IValidator<UpdateMenuItemDto> updateMenuItemValidator = null, IGenericRepository<Category> categoryRepository = null)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
            _addMenuItemValidator = addMenuItemValidator;
            _updateMenuItemValidator = updateMenuItemValidator;
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseDto<object>> AddMenuItem(CreateMenuItemDto dto)
        {
            try
            {
                var validationResult = await _addMenuItemValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                        Data = null,
                        ErrorCode = ErrorCodes.ValidationError
                    };
                    
                }
                var checkCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);
                if (checkCategory == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Eklemek istediğiniz kategori bulunamadı",
                        Data = null,
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var menuItem = _mapper.Map<MenuItem>(dto);
                await _menuItemRepository.AddAsync(menuItem);
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir hata oluştu",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
            // Map CreateMenuItemDto to MenuItem entity
            
            return new ResponseDto<object>
            {
                Success = true,
                Message = "Menu item başarıyla eklendi",
                Data = null
            };
        }

        public async Task<ResponseDto<object>> DeleteMenuItem(int id)
        {
            try
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(id);
                if (menuItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = $"{id} li menu item bulunamadı",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _menuItemRepository.DeleteAsync(menuItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Menu item başarıyla silindi",
                    Data = null
                };
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir hata oluştu",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
            
        }

        public async Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems()
        {
            try
            {
                var menuItems = await _menuItemRepository.GetAllAsync();
                var categories = await _categoryRepository.GetAllAsync();
                if (menuItems == null || !menuItems.Any())
                {
                    return new ResponseDto<List<ResultMenuItemDto>>
                    {
                        ErrorCode = ErrorCodes.NotFound,
                        Success = false,
                        Message = "Hiç menu item bulunamadı",
                        Data = null
                    };
                }
                // Map List<MenuItem> to List<DetailMenuItemDto>
                var menuItemDtos = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
                return new ResponseDto<List<ResultMenuItemDto>>
                {
                    Success = true,
                    Message = $"{menuItemDtos.Count} adet menu item bulundu",
                    Data = menuItemDtos
                };

            }
            catch (Exception)
            {

                return new ResponseDto<List<ResultMenuItemDto>>
                {
                    Success = false,
                    Message = "Bir hata oluştu",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
          
        }

        public async Task<ResponseDto<DetailMenuItemDto>> GetMenuItemById(int id)
        {
            try
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(id);
                var categories = await _categoryRepository.GetByIdAsync(menuItem.CategoryId);
                if (menuItem == null)
                {
                    return new ResponseDto<DetailMenuItemDto>
                    {
                        ErrorCode = ErrorCodes.NotFound,
                        Success = false,
                        Message = $"{id} li menu item bulunamadı",
                        Data = null
                    };
                }
                // Map MenuItem to DetailMenuItemDto
                var menuItemDto = _mapper.Map<DetailMenuItemDto>(menuItem);
                return new ResponseDto<DetailMenuItemDto>
                {
                    Success = true,
                    Message = $"{id} li menu item bulundu",
                    Data = menuItemDto
                };

            }
            catch (Exception)
            {

                return new ResponseDto<DetailMenuItemDto>
                {
                    Success = false,
                    Message = "Bir hata oluştu",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception

                };
            }

            
        }

        public async Task<ResponseDto<object>> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            try
            {
                var menuItemDto = _updateMenuItemValidator.Validate(dto);
                if (!menuItemDto.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = string.Join(", ", menuItemDto.Errors.Select(e => e.ErrorMessage)),
                        Data = null,
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var menuItem = await _menuItemRepository.GetByIdAsync(dto.Id);
                if (menuItem == null)
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = $"{dto.Id} li menu item bulunamadı",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                var checkCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);
                if (checkCategory == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Güncellemek istediğiniz kategori bulunamadı",
                        Data = null,
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }

                // DTO -> mevcut MenuItem (EF Core zaten bu nesneyi takip ediyor)
                var dtoMenuItem =_mapper.Map(dto, menuItem);
               

                await _menuItemRepository.UpdateAsync(menuItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Menu item başarıyla güncellendi",
                    Data = null
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir hata oluştu",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }

            
            
        }

    }
}
