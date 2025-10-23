using KafeApi.Application.Dtos.OrderItemDtos;
using KafeApi.Application.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Abstract
{
    public interface IOrderItemService
    {
        Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems();
        Task<ResponseDto<DetailOrderItemDto>> GetDetailOrderItems(int id);
        Task<ResponseDto<object>> CreateOrderItem(CreateOrderItemDto createOrderItemDto);
        Task<ResponseDto<object>> UpdateOrderItem( UpdateOrderItemDto updateOrderItemDto);
        Task<ResponseDto<object>> DeleteOrderItem(int id);
    }
}
