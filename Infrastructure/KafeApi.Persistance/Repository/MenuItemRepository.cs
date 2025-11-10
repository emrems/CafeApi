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
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext _context;

        public MenuItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetAllMenuItemWithCategoriesAsync()
        {
            var menuItems = await _context.MenuItems
                .Include(mi => mi.Category)
                .ToListAsync();
            return menuItems;
        }
        
    }
}
