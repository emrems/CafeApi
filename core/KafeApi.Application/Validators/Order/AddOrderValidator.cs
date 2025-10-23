using FluentValidation;
using KafeApi.Application.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Validators.Order
{
    public class AddOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public AddOrderValidator()
        {
            RuleFor(x => x.TotalPrice)
            .GreaterThan(0).WithMessage("TotalPrice 0 dan büyük olmalı");
            
        }
    }
}
