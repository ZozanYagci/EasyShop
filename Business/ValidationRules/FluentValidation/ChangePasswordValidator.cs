using DTOs.DTOs.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("Eski şifre boş olamaz");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni şifre boş olamaz")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalı")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermeli")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermeli")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermeli")
                .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermeli");

            RuleFor(x => x.NewPasswordConfirm)
                .NotEmpty().WithMessage("Yeni şifreyi tekrar giriniz")
                .Equal(x => x.NewPassword).WithMessage("Şifreler uyuşmuyor");
        }

    }
}
