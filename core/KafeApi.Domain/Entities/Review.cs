using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string userId { get; set; }// user Identity olsuğu için string
        public string comment { get; set; }
        public int rating { get; set; }
        public DateTime createdAt { get; set; }

    }
}
