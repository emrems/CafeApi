using KafeApi.Application.Interfaces;
using KafeApi.Domain.Entities;
using KafeApi.Persistance.Context;
using Microsoft.EntityFrameworkCore;


namespace KafeApi.Persistance.Repository
{
    public class TableRepository : ITableRepository
    {
       private readonly AppDbContext _context;

        public TableRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Table>> GetActiveTable()
        {
            var tables = _context.Tables.Where(x => x.IsActive).ToListAsync();
            return tables;
            
        }

        public async  Task<Table> GetByTableNumberAsync(int tableNumber)
        {
            var result = await _context.Tables.FirstOrDefaultAsync(x => x.TableNumber == tableNumber);
            return result;

        }
    }
}
