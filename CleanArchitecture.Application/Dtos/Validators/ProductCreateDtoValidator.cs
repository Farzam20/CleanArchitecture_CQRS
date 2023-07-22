using CleanArchitecture.Application.Services.Interfaces;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CleanArchitecture.Application.Dtos.Validators
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("وارد کردن نام محصول الزامی است")
                .NotNull()
                .WithMessage("وارد کردن نام محصول الزامی است");

            RuleFor(x => x.ManufactureEmail)
                .NotEmpty()
                .WithMessage("وارد کردن ایمیل سازنده الزامی است")
                .NotNull()
                .WithMessage("وارد کردن ایمیل سازنده الزامی است")
                .EmailAddress()
                .WithMessage("ایمیل وارد شده معتبر نمی باشد");

            RuleFor(x => x.ManufacturePhone)
                .NotEmpty()
                .WithMessage("وارد کردن شماره تماس سازنده الزامی است")
                .NotNull()
                .WithMessage("وارد کردن شماره تماس سازنده الزامی است")
                .Matches(new Regex("^(\\+98|0)?9\\d{9}$"))
                .WithMessage("شماره تماس وارد شده معتبر نمی باشد");

            RuleFor(x => x.ProduceDate)
                .NotEmpty()
                .WithMessage("وارد کردن تاریخ تولید الزامی است")
                .NotNull()
                .WithMessage("وارد کردن تاریخ تولید الزامی است");

        }
    }
}