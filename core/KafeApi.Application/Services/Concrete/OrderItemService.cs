using AutoMapper;
using FluentValidation;
using KafeApi.Application.Dtos.OrderItemDtos;
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
    public class OrderItemService : IOrderItemService
    {
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderItemDto> _CreateOrderItemDtoValidator;
        private readonly IValidator<UpdateOrderItemDto> _updateOrderItemDtoValidator;

        public OrderItemService(IGenericRepository<OrderItem> orderItemRepository, IMapper mapper, IValidator<CreateOrderItemDto> createOrderItemDtoValidator, IValidator<UpdateOrderItemDto> updateOrderItemDtoValidator)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
            _CreateOrderItemDtoValidator = createOrderItemDtoValidator;
            _updateOrderItemDtoValidator = updateOrderItemDtoValidator;
        }

        public async Task<ResponseDto<object>> CreateOrderItem(CreateOrderItemDto createOrderItemDto)
        {
            var validationResult = await _CreateOrderItemDtoValidator.ValidateAsync(createOrderItemDto);
            if (!validationResult.IsValid)
            {
                return new ResponseDto<object>
                {
                    Data = null,
                    Success = false,
                    Message = string.Join("",validationResult.Errors.Select(x => x.ErrorMessage)),
                    ErrorCode = ErrorCodes.ValidationError
                };
            }
            var orderItem = _mapper.Map<OrderItem>(createOrderItemDto);
            await _orderItemRepository.AddAsync(orderItem);
            return new ResponseDto<object>
            {
                Data = null,
                Success = true,
                Message = "Order item created successfully.",
                ErrorCode = null
            };
        }

        public async Task<ResponseDto<object>> DeleteOrderItem(int id)
        {
            try
            {
                var orderItem = await _orderItemRepository.GetByIdAsync(id);
                if (orderItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Success = false,
                        Message = "Belirtilen id'ye sahip order item bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _orderItemRepository.DeleteAsync(orderItem);
                return new ResponseDto<object>
                {
                    Data = null,
                    Success = true,
                    Message = "Order item başarıyla silindi.",
                    ErrorCode = null
                };

            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Success = false,
                    Message = "Bir hata oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems()
        {
            try
            {
                var orderItems = await _orderItemRepository.GetAllAsync();
                if(orderItems == null || !orderItems.Any())
                {
                    return new ResponseDto<List<ResultOrderItemDto>>
                    {
                        Data = null,
                        Success = false,
                        Message = "Herhangi bir order items bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var resultOrderItemsDto = _mapper.Map<List<ResultOrderItemDto>>(orderItems);
                return new ResponseDto<List<ResultOrderItemDto>>
                {
                    Data = resultOrderItemsDto,
                    Success = true,
                    Message = "Order itemslar çekildi.",
                    ErrorCode = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultOrderItemDto>>
                {
                    Data = null,
                    Success = false,
                    Message = $"bir hata: {ex.Message}",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailOrderItemDto>> GetDetailOrderItems(int id)
        {
            try
            {
                var orderItem = await _orderItemRepository.GetByIdAsync(id);
                if (orderItem == null)
                {
                    return new ResponseDto<DetailOrderItemDto>
                    {
                        Data = null,
                        Success = false,
                        Message = "Belirtilen id'ye sahip order item bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var detailOrderItemDto = _mapper.Map<DetailOrderItemDto>(orderItem);
                return new ResponseDto<DetailOrderItemDto>
                {
                    Data = detailOrderItemDto,
                    Success = true,
                    Message = "Order item detayı başarıyla getirildi.",
                    ErrorCode = null
                };
            }
            catch (Exception)
            {

                return new ResponseDto<DetailOrderItemDto>
                {
                    Data = null,
                    Success = false,
                    Message = "Bir hata oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderItem(UpdateOrderItemDto updateOrderItemDto)
        {
            try
            {
                var validationResult = await _updateOrderItemDtoValidator.ValidateAsync(updateOrderItemDto);
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Success = false,
                        Message = string.Join("", validationResult.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var orderItem = await _orderItemRepository.GetByIdAsync(updateOrderItemDto.Id);
                if (orderItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Success = false,
                        Message = "Belirtilen id'ye sahip order item bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
               var result = _mapper.Map(updateOrderItemDto, orderItem);
                await _orderItemRepository.UpdateAsync(orderItem);
                return new ResponseDto<object>
                {
                    Data = result,
                    Success = true,
                    Message = "Order item başarıyla güncellendi.",
                    ErrorCode = null
                };
            }
            catch (Exception)
            {
                return new ResponseDto<object>
                {
                    Data = null,
                    Success = false,
                    Message = "Bir hata oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
