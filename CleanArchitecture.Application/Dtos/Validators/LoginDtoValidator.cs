using FluentValidation;

namespace CleanArchitecture.Application.Dtos.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("وارد کردن نام کاربری الزامی است")
                .NotNull()
                .WithMessage("وارد کردن نام کاربری الزامی است");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("وارد کردن کلمه عبور الزامی است")
                .NotNull()
                .WithMessage("وارد کردن کلمه عبور الزامی است");
        }
    }
}