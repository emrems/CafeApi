using AutoMapper;
using FluentValidation;
using KafeApi.Application.Dtos.OrderDtos;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IOrderRepository _customOrderRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderDto> _orderValidator;

        public OrderService(IGenericRepository<Order> orderRepository, IMapper mapper, IValidator<CreateOrderDto> orderValidator = null, IOrderRepository customOrderRepository = null, IGenericRepository<MenuItem> menuItemRepository = null, IGenericRepository<Table> tableRepository = null)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderValidator = orderValidator;
            _customOrderRepository = customOrderRepository;
            _menuItemRepository = menuItemRepository;
            _tableRepository = tableRepository;
        }

        //public async Task<ResponseDto<object>> AddOrderItemByOrderId(AddOrderItemByOrder dto)
        //{
        //    try
        //    {
        //        var order = await _orderRepository.GetByIdAsync(dto.OrderId);
        //        if (order == null)
        //        {
        //            return new ResponseDto<object>
        //            {
        //                Success = false,
        //                Message = "Sipariş bulunamadı",
        //                Data = null,
        //                ErrorCode = ErrorCodes.NotFound
        //            };
        //        }
        //        var menuItem = await _menuItemRepository.GetByIdAsync(dto.CreateOrderItemDto.menuItemId);
        //        if (menuItem == null)
        //        {
        //            return new ResponseDto<object>
        //            {
        //                Success = false,
        //                Message = "Menü öğesi bulunamadı",
        //                Data = null,
        //                ErrorCode = ErrorCodes.NotFound
        //            };
        //        }
        //        var orderItem = new OrderItem
        //        {
        //            menuItemId = dto.CreateOrderItemDto.menuItemId,
        //            Quantity = dto.CreateOrderItemDto.Quantity,
        //            Price = menuItem.Price * dto.CreateOrderItemDto.Quantity,
        //            MenuItem = menuItem
        //        };
        //        order.OrderItems.Add(orderItem);
        //        order.TotalPrice += orderItem.Price;
        //        await _orderRepository.UpdateAsync(order);
        //        return new ResponseDto<object>
        //        {
        //            Success = true,
        //            Message = "Sipariş öğesi başarıyla eklendi",
        //            Data = null,
        //            ErrorCode = null
        //        };

        //    }
        //    catch (Exception)
        //    {

        //        return new ResponseDto<object>
        //        {
        //            Success = false,
        //            Message = "bir hata oldu",
        //            Data = null,
        //            ErrorCode = ErrorCodes.Exception
        //        };
        //    }
        //}

        public async Task<ResponseDto<object>> CancelOrder(int id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                if (order == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Sipariş bulunamadı",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                order.status = OrderStatus.Cancelled.ToString();
                await _orderRepository.UpdateAsync(order);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Sipariş durumu iptal edildi olarak  güncellendi",
                    Data = null,
                    ErrorCode = null
                };

            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "bir hata oldu",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> CompleteOrder(int id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                if (order == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Sipariş bulunamadı",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                order.status = OrderStatus.Completed.ToString();
                await _orderRepository.UpdateAsync(order);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Sipariş durumu tamamlandı olarak güncellendi",
                    Data = null,
                    ErrorCode = null
                };

            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "bir hata oldu",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> CreateOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                var validateOrder = _orderValidator.ValidateAsync(createOrderDto);
                if (!validateOrder.Result.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = string.Join(", ", validateOrder.Result.Errors.Select(e => e.ErrorMessage)),
                        Data = null,
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var order = _mapper.Map<Order>(createOrderDto);
                decimal totalPrice = 0;
                foreach (var item in order.OrderItems)
                {
                    item.MenuItem = await _menuItemRepository.GetByIdAsync(item.menuItemId);
                    if (item.MenuItem == null)
                    {
                        throw new Exception($"MenuItem with ID {item.menuItemId} not found.");
                    }
                    item.Price = item.MenuItem.Price * item.Quantity;
                    totalPrice += item.Price;


                }
                order.TotalPrice = totalPrice;
                order.status = OrderStatus.Pending.ToString();
                await _orderRepository.AddAsync(order);
                var table = await _tableRepository.GetByIdAsync(createOrderDto.TableId);
                table.IsActive =false;
                await _tableRepository.UpdateAsync(table);

                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Sipariş başarıyla oluşturuldu",
                    Data = null,
                    ErrorCode = null
                };

            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "bir hata oldu",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
                
            }
        }

        public async Task<ResponseDto<object>> DeleteOrder(int id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                if (order == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Sipariş bulunamadı",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _orderRepository.DeleteAsync(order);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Sipariş başarıyla silindi",
                    Data = null,
                    ErrorCode = null
                };

            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "bir hata oldu",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

      
        public async Task<ResponseDto<DetailOrderDto>> GetDetailById(int id)
        {
            try
            {
                var order = await _customOrderRepository.GetOrderWithDetail(id);
               
                if (order == null)
                {
                    return new ResponseDto<DetailOrderDto>
                    {
                        Success = false,
                        Message = "Sipariş bulunamadı",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var detailOrderDto = _mapper.Map<DetailOrderDto>(order);
                return new ResponseDto<DetailOrderDto>
                {
                    Success = true,
                    Message = "Sipariş başarıyla getirildi",
                    Data = detailOrderDto,
                    ErrorCode = null
                };

            }
            catch (Exception)
            {

                return new ResponseDto<DetailOrderDto>
                {
                    Success = false,
                    Message = "bir hata oldu",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
            
        }

        public async Task<ResponseDto<List<ResultOrderDto>>> GettAllOrders()
        {
            try
            {
                var orders = await _customOrderRepository.GetAllOrderByOrderRepository();
                var resultOrderDtos = _mapper.Map<List<ResultOrderDto>>(orders);
                if(resultOrderDtos == null || resultOrderDtos.Count == 0)
                {
                    return new ResponseDto<List<ResultOrderDto>>
                    {
                        Success = false,
                        Message = "Hiç sipariş bulunamadı",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                return new ResponseDto<List<ResultOrderDto>>
                {
                    Success = true,
                    Message = "Siparişler başarıyla getirildi",
                    Data = resultOrderDtos,
                    ErrorCode = null
                };

            }
            catch (Exception)
            {

                return new ResponseDto<List<ResultOrderDto>>
                {
                    Success = false,
                    Message = "bir hata oldu",
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            try
            {
                var order = await _customOrderRepository.GetOrderWithDetail(updateOrderDto.Id);

                if (order == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Sipariş bulunamadı",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                // Masa bilgisi değiştiyse güncelle
                order.TableId = updateOrderDto.TableId;
                order.UpdatedAt = DateTime.UtcNow;

                // Tüm ürünleri kontrol et (hem var olan hem yeni eklenen)
                foreach (var itemDto in updateOrderDto.OrderItems)
                {
                    var menuItem = await _menuItemRepository.GetByIdAsync(itemDto.menuItemId);
                    if (menuItem == null)
                        continue;

                    // Mevcut item var mı kontrol et
                    var existingItem = order.OrderItems.FirstOrDefault(x => x.menuItemId == itemDto.menuItemId);

                    if (existingItem != null)
                    {
                        // Varsa sadece miktarı ve fiyatı güncelle
                        existingItem.Quantity = itemDto.Quantity;
                        existingItem.Price = menuItem.Price * itemDto.Quantity;
                    }
                    else
                    {
                        // Yoksa yeni bir ürün olarak ekle
                        var newItem = new OrderItem
                        {
                            menuItemId = itemDto.menuItemId,
                            Quantity = itemDto.Quantity,
                            Price = menuItem.Price * itemDto.Quantity
                        };
                        order.OrderItems.Add(newItem);
                    }
                }

                // Artık toplam tutarı yeniden hesapla
                order.TotalPrice = order.OrderItems.Sum(x => x.Price);

                await _orderRepository.UpdateAsync(order);

                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Sipariş başarıyla güncellendi",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir hata oluştu: " + ex.Message,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

    }
}
