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
        private readonly IOrderRepository _customOrderRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderDto> _orderValidator;

        public OrderService(IGenericRepository<Order> orderRepository, IMapper mapper, IValidator<CreateOrderDto> orderValidator = null, IOrderRepository customOrderRepository = null)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderValidator = orderValidator;
            _customOrderRepository = customOrderRepository;
           
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
                await _orderRepository.AddAsync(order);
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

        public async Task<ResponseDto<object>> UpdateOrder( UpdateOrderDto updateOrderDto)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(updateOrderDto.Id);
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
                _mapper.Map(updateOrderDto, order);
                await _orderRepository.UpdateAsync(order);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Sipariş başarıyla güncellendi",
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
    }
}
