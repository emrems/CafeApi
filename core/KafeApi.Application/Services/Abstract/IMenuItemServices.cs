using KafeApi.Application.Dtos.MenuItemDto;
using KafeApi.Application.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Abstract
{
    public interface IMenuItemServices
    {
        Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems();
        Task<ResponseDto<DetailMenuItemDto>> GetMenuItemById(int id);
        Task AddMenuItem(CreateMenuItemDto dto);
        Task UpdateMenuItem(UpdateMenuItemDto dto);
        Task DeleteMenuItem(int id);
    }
}
