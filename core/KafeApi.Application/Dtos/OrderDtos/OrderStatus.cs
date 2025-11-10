using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Dtos.OrderDtos
{
    public static class OrderStatus
    {
        public const string Pending = "Beklemede";
        public const string InProgress = "Devam ediyor";
        public const string Completed = "Tamamlandı";
        public const string Cancelled = "İptal edildi";
        public const string Paid = "Ödendi";
    }
}
