using CleanArchitecture.Application.CQRS.ProductFiles.Commands;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Services.Interfaces;
using CleanArchitecture.Domain.Entities;
using Mapster;
using MediatR;

namespace CleanArchitecture.Application.CQRS.ProductFiles.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, HandlerResponse<ProductDisplayDto>>
    {
        private readonly IProductService _productService;

        public UpdateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<HandlerResponse<ProductDisplayDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var obj = await _productService.GetByIdAsync(cancellationToken, request.Product.Id);

            if (obj == null)
                return new(false, "محصول مورد نظر یافت نشد", null);

            if (!(await _productService.ProductIsUnique(obj)))
                return new(false, "محصول وارد شده تکراری می باشد", null);

            if (obj.CreatedByUserId != request.CurrentUserId)
                return new(false, "امکان ویرایش این محصول توسط شما وجود ندارد", null);

            request.Product.Adapt(obj);
            var result = await _productService.UpdateAsync(obj, cancellationToken);
            return result.Adapt<ProductDisplayDto>();
        }
    }
}
