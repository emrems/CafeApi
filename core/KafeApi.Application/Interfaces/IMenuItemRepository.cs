using KafeApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<List<MenuItem>> GetAllMenuItemWithCategoriesAsync();
    }
}
