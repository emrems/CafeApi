using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Domain.Entities
{
    public class Table
    {
        public int Id { get; set; }

        public int TableNumber { get; set; }
        public bool IsActive { get; set; }
        public int Capacity { get; set; }
    }
}
