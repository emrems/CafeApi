using KafeApi.Application.Dtos.MenuItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Dtos.OrderItemDtos
{
    public class UpdateOrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int menuItemId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
