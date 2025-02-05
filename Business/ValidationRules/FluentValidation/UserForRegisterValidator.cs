using DTOs.DTOs.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserForRegisterValidator: AbstractValidator<UserForRegisterDto>    
    {
        public UserForRegisterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad alanı boş olamaz")
                .MinimumLength(2).WithMessage("Ad alanı en az 2 karakter olmalıdır");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad alanı boş olamaz");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı boş olamaz")
                .MinimumLength(6).WithMessage("Şifre alanı en az 6 karakter olmalıdır")
                .Matches(@"[A-Z]+").WithMessage("Şifre en az bir büyük harf içermelidir")
                .Matches(@"\d").WithMessage("Şifre en az bir rakam içermelidir")
                .Matches(@"[\W]").WithMessage("Şifre en az bir özel karakter içermelidir");
        }
    }
}
