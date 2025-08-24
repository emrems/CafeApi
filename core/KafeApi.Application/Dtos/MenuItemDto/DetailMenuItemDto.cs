using KafeApi.Application.Dtos.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Dtos.MenuItemDto
{
    public class DetailMenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAviable { get; set; }
        public int CategoryId { get; set; }

        public ResultCategoryDto Category { get; set; }
    }
}
