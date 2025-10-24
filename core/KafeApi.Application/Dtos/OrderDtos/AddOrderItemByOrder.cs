using KafeApi.Application.Dtos.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Dtos.OrderDtos
{
    public class AddOrderItemByOrder
    {
        public int OrderId { get; set; }
        public CreateOrderItemDto CreateOrderItemDto { get; set; }
    }
}
