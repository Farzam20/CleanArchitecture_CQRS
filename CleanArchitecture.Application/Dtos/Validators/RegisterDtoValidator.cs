using FluentValidation;
using System.Text.RegularExpressions;

namespace CleanArchitecture.Application.Dtos.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("وارد کردن نام کاربری الزامی است")
                .NotNull()
                .WithMessage("وارد کردن نام کاربری الزامی است");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("وارد کردن ایمیل الزامی است")
                .NotNull()
                .WithMessage("وارد کردن ایمیل الزامی است")
                .EmailAddress()
                .WithMessage("ایمیل وارد شده معتبر نمی باشد");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("وارد کردن شماره تماس الزامی است")
                .NotNull()
                .WithMessage("وارد کردن شماره تماس الزامی است")
                .Matches(new Regex("^(\\+98|0)?9\\d{9}$"))
                .WithMessage("شماره تماس وارد شده معتبر نمی باشد");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("وارد کردن کلمه عبور الزامی است")
                .NotNull()
                .WithMessage("وارد کردن کلمه عبور الزامی است")
                .MinimumLength(6)
                .WithMessage("حداقل طول مجاز برای کلمه عبور 6 کاراکتر می باشد")
                .Must(x => x.Any(char.IsDigit))
                .WithMessage("کلمه عبور باید حداقل شامل یک عدد باشد");

            RuleFor(x => x.RePassword)
                .Equal(x => x.Password)
                .WithMessage("تکرار کلمه عبور اشتباه است");
        }
    }
}