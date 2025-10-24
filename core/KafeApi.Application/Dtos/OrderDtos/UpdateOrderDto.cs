using KafeApi.Application.Dtos.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Dtos.OrderDtos
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
        //public string status { get; set; }
        //public decimal TotalPrice { get; set; }
        public int TableId { get; set; }
        public List<UpdateOrderItemDto> OrderItems { get; set; }
    }
}
