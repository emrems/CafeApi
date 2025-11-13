using KafeApi.Application.Dtos.OrderDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController

    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GettAllOrders();
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailOrders(int id)
        {
            var result = await _orderService.GetDetailById(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            var result = await _orderService.CreateOrder(createOrderDto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDto updateOrderDto)
        {
            var result = await _orderService.UpdateOrder(updateOrderDto);
            return CreateResponse(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);
            return CreateResponse(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("complete/{id}")]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            var result = await _orderService.CompleteOrder(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var result = await _orderService.CancelOrder(id);
            return CreateResponse(result);
        }

        //[HttpPost("add-order-item")]
        //public async Task<IActionResult> AddOrderItemByOrderId([FromBody] AddOrderItemByOrder dto)
        //{
        //    var result = await _orderService.AddOrderItemByOrderId(dto);
        //    return CreateResponse(result);

        //}

        [Authorize(Roles = "Admin")]

        [HttpPut("paid")]
        public async Task<IActionResult> MarkOrderAsPaid(int id)
        {
            var result = await _orderService.UpdateOrderStatusPaid(id);
            return CreateResponse(result);
        }
    }
}
