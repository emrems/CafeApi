using FluentValidation;
using KafeApi.Application.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Validators.User
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim alanı boş olamaz.")
                .MaximumLength(50).WithMessage("İsim alanı en fazla 50 karakter olabilir.");
            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyisim alanı boş olamaz.")
                .MaximumLength(50).WithMessage("Soyisim alanı en fazla 50 karakter olabilir.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon alanı boş olamaz.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Geçerli bir telefon numarası giriniz.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola alanı boş olamaz.")
                .MinimumLength(6).WithMessage("Parola en az 6 karakter olmalıdır.")
                .Matches("[A-Z]").WithMessage("Parola en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Parola en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Parola en az bir rakam içermelidir.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Parola en az bir özel karakter içermelidir.");
        }
    }
}
