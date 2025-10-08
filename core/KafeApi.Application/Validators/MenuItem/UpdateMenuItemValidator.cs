using FluentValidation;
using KafeApi.Application.Dtos.MenuItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Validators.MenuItem
{
    public class UpdateMenuItemValidator : AbstractValidator<UpdateMenuItemDto>
    {
        public UpdateMenuItemValidator()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("MenuItem adı boş olamaz")
           .Length(3, 50).WithMessage("MenuItem uzunluğu 3 ile 50 arasında olmalı");
            RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("MenuItem f 0 dan büyük olmalı");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("MenuItem açıklaması boş olamaz")
                .MaximumLength(200).WithMessage("MenuItem açıklaması en fazla 200 karakter olabilir");
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("MenuItem resim URL'si boş olamaz");
        }
    }
}
