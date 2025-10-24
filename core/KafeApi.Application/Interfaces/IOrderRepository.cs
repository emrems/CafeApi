using KafeApi.Application.Dtos.OrderItemDtos;
using KafeApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrderByOrderRepository();
        Task<Order> GetOrderWithDetail(int orderId);
    }
}
