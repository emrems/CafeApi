using FluentValidation;
using KafeApi.Application.Dtos.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Validators.OrderItem
{
    public class UpdateOrderItemValidator : AbstractValidator<UpdateOrderItemDto>
    {
        public UpdateOrderItemValidator()
        {
            RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity 0 dan büyük olmalı")
            .NotEmpty().WithMessage("Quantity boş olamaz");
        }
    }
}
