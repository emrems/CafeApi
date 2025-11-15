using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Dtos.ReviewDtos
{
    public class CreateReviewDto
    {
        public string userId { get; set; }
        public string comment { get; set; }
        public int rating { get; set; }
        public DateTime createdAt { get; set; }
    }
}
