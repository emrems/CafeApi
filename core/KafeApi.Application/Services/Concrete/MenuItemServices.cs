using AutoMapper;
using KafeApi.Application.Dtos.MenuItemDto;
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

        public async Task<List<DetailMenuItemDto>> GetAllMenuItems()
        {
            var menuItems =  await _menuItemRepository.GetAllAsync();
            if (menuItems == null || !menuItems.Any())
            {
                menuItems = new List<MenuItem>();
            }
            // Map List<MenuItem> to List<DetailMenuItemDto>
            var menuItemDtos = _mapper.Map<List<DetailMenuItemDto>>(menuItems);
            return menuItemDtos;
        }

        public async Task<DetailMenuItemDto> GetMenuItemById(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            if (menuItem == null)
            {
                throw new KeyNotFoundException($"{id} li menu item bulunamadı");
            }
            // Map MenuItem to DetailMenuItemDto
            var menuItemDto = _mapper.Map<DetailMenuItemDto>(menuItem);
            return menuItemDto;
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
