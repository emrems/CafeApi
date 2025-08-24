using KafeApi.Application.Dtos.MenuItemDto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Dtos.CategoryDto
{
    public class DetailCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ResultMenuItemDto> MenuItems { get; set; } = new List<ResultMenuItemDto>();

    }
}
