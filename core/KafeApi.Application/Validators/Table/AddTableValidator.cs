using FluentValidation;
using KafeApi.Application.Dtos.TableDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Validators.Table
{
    public class AddTableValidator : AbstractValidator<CreateTableDto>
    {
        public AddTableValidator()
        {
            RuleFor(x => x.TableNumber).NotEmpty().WithMessage("Masa numarası boş geçilemez");
            RuleFor(x => x.Capacity).NotEmpty().WithMessage("Kapasite boş geçilemez");
            RuleFor(x => x.Capacity).GreaterThan(0).WithMessage("Kapasite 0'dan büyük olmalıdır");
        }
    }
}
