using KafeApi.Application.Dtos.MenuItemDto;
using KafeApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Dtos.CategoryDto
{
    public class ResultCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoriesMenUItemDto> MenuItems { get; set; }

    }
}
