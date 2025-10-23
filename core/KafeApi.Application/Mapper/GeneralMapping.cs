using AutoMapper;
using KafeApi.Application.Dtos.CategoryDto;
using KafeApi.Application.Dtos.MenuItemDto;
using KafeApi.Application.Dtos.OrderDtos;
using KafeApi.Application.Dtos.OrderItemDtos;
using KafeApi.Application.Dtos.TableDtos;
using KafeApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Mapper
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<CreateCategoryDto, Category>().ReverseMap();
            CreateMap<ResultCategoryDto, Category>().ReverseMap();
            CreateMap<DetailCategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();

            CreateMap<CreateMenuItemDto, MenuItem>().ReverseMap();
            CreateMap<ResultMenuItemDto, MenuItem>().ReverseMap();
            CreateMap<DetailMenuItemDto, MenuItem>().ReverseMap();
            CreateMap<UpdateMenuItemDto, MenuItem>().ReverseMap();

            CreateMap<Table,ResultTableDto>().ReverseMap();
            CreateMap<Table, DetailTableDto>().ReverseMap();
            CreateMap<CreateTableDto, Table>().ReverseMap();

            CreateMap<CreateOrderItemDto, OrderItem>().ReverseMap();
            CreateMap<ResultOrderItemDto, OrderItem>().ReverseMap();
            CreateMap<DetailOrderItemDto, OrderItem>().ReverseMap();
            CreateMap<UpdateOrderItemDto, OrderItem>().ReverseMap();

            CreateMap<CreateOrderDto,Order>().ReverseMap();
            CreateMap<DetailOrderDto, Order>().ReverseMap();
            CreateMap<ResultOrderDto, Order>().ReverseMap();
            CreateMap<UpdateOrderDto, Order>().ReverseMap();

        }
    }
   
}
