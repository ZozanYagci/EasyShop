using DTOs.DTOs.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserProfileValidator : AbstractValidator<UserProfileUpdateDto>
    {
        public UserProfileValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad alanı boş bırakılamaz.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad alanı boş bırakılamaz.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta boş bırakılamaz")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz");


            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon bilgisi boş bırakılamaz.")
                .Matches(@"^\d{10}$").WithMessage("Geçerli bir telefon numarası giriniz");
        }
    }
}
