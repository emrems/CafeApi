using FluentValidation;
using KafeApi.Application.Dtos.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Validators.Category
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator() 
        { 
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz")
                .Length(3, 50).WithMessage("Kategori adı 3 ile 50 karakter arasında olmalıdır");

        }
    }
}
