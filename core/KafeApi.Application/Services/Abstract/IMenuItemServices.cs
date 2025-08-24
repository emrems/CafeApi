using KafeApi.Application.Dtos.MenuItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Abstract
{
    public interface IMenuItemServices
    {
        Task<List<DetailMenuItemDto>> GetAllMenuItems();
        Task<DetailMenuItemDto> GetMenuItemById(int id);
        Task AddMenuItem(CreateMenuItemDto dto);
        Task UpdateMenuItem(UpdateMenuItemDto dto);
        Task DeleteMenuItem(int id);
    }
}
