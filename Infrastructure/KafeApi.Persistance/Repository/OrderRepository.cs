using KafeApi.Application.Interfaces;
using KafeApi.Domain.Entities;
using KafeApi.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Persistance.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrderByOrderRepository()
        {
            var orders = await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x =>x.MenuItem)
                .ThenInclude(x => x.Category)
                .ToListAsync();
            return orders;
        }

        public async Task<Order> GetOrderWithDetail(int orderId)
        {
            var order = await _context.Orders
                 .Where(x => x.Id == orderId)
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.MenuItem)
                .ThenInclude( x => x.Category)
                .FirstOrDefaultAsync();
            return order;
        }
    }
}
