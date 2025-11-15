using FluentValidation;
using KafeApi.Application.Dtos.ReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Validators.Review
{
    public class CreateReviewValidator : AbstractValidator<CreateReviewDto>
    {
        public CreateReviewValidator()
        {
            RuleFor(r => r.userId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.")
                .MaximumLength(100).WithMessage("Kullanıcı ID en fazla 100 karakter olabilir.");
            RuleFor(r => r.comment)
                .NotEmpty().WithMessage("Yorum boş olamaz.")
                .MaximumLength(1000).WithMessage("Yorum en fazla 1000 karakter olabilir.");
            RuleFor(r => r.rating)
                .InclusiveBetween(1, 5).WithMessage("Puan 1 ile 5 arasında olmalıdır.");
            RuleFor(r => r.createdAt)
                .NotEmpty().WithMessage("Oluşturulma tarihi boş olamaz.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Oluşturulma tarihi gelecekte olamaz.");
        }
    }
}
