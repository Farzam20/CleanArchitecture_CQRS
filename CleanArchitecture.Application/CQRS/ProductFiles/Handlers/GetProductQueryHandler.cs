using CleanArchitecture.Application.CQRS.ProductFiles.Queries;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Services.Interfaces;
using Mapster;
using MediatR;

namespace CleanArchitecture.Application.CQRS.ProductFiles.Handlers
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, HandlerResponse<ProductDisplayDto>>
    {
        private readonly IProductService _productService;

        public GetProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<HandlerResponse<ProductDisplayDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetByIdAsync(cancellationToken, request.Id);

            if (product == null)
                return new(false, "محصول موردنظر یافت نشد", null);

            return product.Adapt<ProductDisplayDto>();
        }
    }
}
