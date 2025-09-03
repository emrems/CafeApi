using AutoMapper;
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

        public MenuItemServices(IGenericRepository<MenuItem> menuItemRepository, IMapper mapper = null)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task AddMenuItem(CreateMenuItemDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "MenuItem boş olamaz");

            }
            // Map CreateMenuItemDto to MenuItem entity
            var menuItem = _mapper.Map<MenuItem>(dto);
            await _menuItemRepository.AddAsync(menuItem);
        }

        public async Task DeleteMenuItem(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            if (menuItem == null)
            {
                throw new KeyNotFoundException($"{id} li menu item bulunamadı");
            }
            await _menuItemRepository.DeleteAsync(menuItem);
        }

        public async Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems()
        {
            try
            {
                var menuItems = await _menuItemRepository.GetAllAsync();
                if (menuItems == null || !menuItems.Any())
                {
                    return new ResponseDto<List<ResultMenuItemDto>>
                    {
                        ErrorCodes = ErrorCodes.NotFound,
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
                    ErrorCodes = ErrorCodes.Exception
                };
            }
          
        }

        public async Task<ResponseDto<DetailMenuItemDto>> GetMenuItemById(int id)
        {
            try
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(id);
                if (menuItem == null)
                {
                    return new ResponseDto<DetailMenuItemDto>
                    {
                        ErrorCodes = ErrorCodes.NotFound,
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
                    ErrorCodes = ErrorCodes.Exception

                };
            }

            
        }

        public async Task UpdateMenuItem(UpdateMenuItemDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "MenuItem boş olamaz");

            var menuItem = await _menuItemRepository.GetByIdAsync(dto.Id);
            if (menuItem == null)
                throw new KeyNotFoundException($"{dto.Id} li menu item bulunamadı");

            // DTO -> mevcut MenuItem (EF Core zaten bu nesneyi takip ediyor)
            _mapper.Map(dto, menuItem);

            await _menuItemRepository.UpdateAsync(menuItem);
        }

    }
}
