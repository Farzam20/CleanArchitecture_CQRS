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
            var product = request.Product.Adapt<Product>();

            if (!(await _productService.ProductIsUnique(product)))
                return new(false, "محصول وارد شده تکراری می باشد", null);

            if (product.CreatedByUserId != request.CurrentUserId)
                return new(false, "امکان ویرایش این محصول توسط شما وجود ندارد", null);

            var result = await _productService.UpdateAsync(product, cancellationToken);
            return result.Adapt<ProductDisplayDto>();
        }
    }
}
