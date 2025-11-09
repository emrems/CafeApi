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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> getCategoriesByNameAsync(string name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == name);
            return category;
        }

        public async Task<List<Category>> getCategoriesDetails()
        {
            var categories = await _context.Categories
                .Include(x=> x.MenuItems)
                .ToListAsync();
            return categories;
        }

        public async Task<Category> getCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(x => x.MenuItems)
                .FirstOrDefaultAsync(x => x.Id == id);
            return category;
        }
    }
}
