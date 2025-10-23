using KafeApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> getCategoriesByNameAsync(string name);
    }
}
