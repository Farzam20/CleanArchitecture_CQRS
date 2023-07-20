using CleanArchitecture.Application.CQRS.ProductFiles.Commands;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Services.Interfaces;
using CleanArchitecture.Domain.Entities;
using Mapster;
using MediatR;

namespace CleanArchitecture.Application.CQRS.ProductFiles.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, HandlerResponse<ProductDisplayDto>>
    {
        private readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<HandlerResponse<ProductDisplayDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.Product.Adapt<Product>();

            if (!(await _productService.ProductIsUnique(product)))
                return new(false, "محصول وارد شده تکراری می باشد", null);

            var result = await _productService.AddAsync(product, cancellationToken);
            return result.Adapt<ProductDisplayDto>();
        }
    }
}
