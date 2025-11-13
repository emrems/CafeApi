//using KafeApi.Application.Dtos.OrderItemDtos;
//using KafeApi.Application.Services.Abstract;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace KafeApi.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrderItemController : BaseController
//    {
//        private readonly IOrderItemService _orderItemService;

//        public OrderItemController(IOrderItemService orderItemService)
//        {
//            _orderItemService = orderItemService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllOrderItems()
//        {
//            var result = await _orderItemService.GetAllOrderItems();
//            return CreateResponse(result);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetDetailOrderItems(int id)
//        {
//            var result = await _orderItemService.GetDetailOrderItems(id);
//            return CreateResponse(result);
//        }
//        [HttpPost]
//        public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItemDto createOrderItemDto)
//        {
//            var result = await _orderItemService.CreateOrderItem(createOrderItemDto);
//            return CreateResponse(result);
//        }
//        [HttpPut]
//        public async Task<IActionResult> UpdateOrderItem([FromBody] UpdateOrderItemDto updateOrderItemDto)
//        {
//            var result = await _orderItemService.UpdateOrderItem(updateOrderItemDto);
//            return CreateResponse(result);
//        }
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteOrderItem(int id)
//        {
//            var result = await _orderItemService.DeleteOrderItem(id);
//            return CreateResponse(result);
//        }
//    }
//}
