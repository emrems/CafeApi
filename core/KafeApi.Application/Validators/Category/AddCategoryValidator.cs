using FluentValidation;
using KafeApi.Application.Dtos.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Validators.Category
{
    public class AddCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public AddCategoryValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("kategori adı boş olamaz")
            .Length(3, 50).WithMessage("kategori adı 3 ile 50 arasında olmalı");
            
            
        }
    }
}
