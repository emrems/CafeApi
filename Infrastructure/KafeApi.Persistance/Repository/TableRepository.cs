﻿using KafeApi.Application.Interfaces;
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
    public class TableRepository :  ITableRepository
    {
       private readonly AppDbContext _context;

        public TableRepository(AppDbContext context)
        {
            _context = context;
        }

        public async  Task<Table> GetByTableNumberAsync(int tableNumber)
        {
            var result = await _context.Tables.FirstOrDefaultAsync(x => x.TableNumber == tableNumber);
            return result;

        }
    }
}
