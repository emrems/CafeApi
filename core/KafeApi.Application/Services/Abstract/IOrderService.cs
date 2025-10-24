using KafeApi.Application.Dtos.OrderDtos;
using KafeApi.Application.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Abstract
{
    public interface IOrderService
    {
        Task<ResponseDto<List<ResultOrderDto>>> GettAllOrders();
        Task<ResponseDto<DetailOrderDto>> GetDetailById(int id);
        Task<ResponseDto<object>> CreateOrder(CreateOrderDto createOrderDto);
        Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto updateOrderDto);
        Task<ResponseDto<object>> DeleteOrder(int id);
        Task<ResponseDto<object>> CompleteOrder(int id);
        Task<ResponseDto<object>> CancelOrder(int id);

    }
}
