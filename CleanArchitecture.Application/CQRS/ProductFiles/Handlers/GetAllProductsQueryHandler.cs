using CleanArchitecture.Application.CQRS.ProductFiles.Queries;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.CQRS.ProductFiles.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, HandlerResponse<List<ProductDisplayDto>>>
    {
        private readonly IProductService _productService;

        public GetAllProductsQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<HandlerResponse<List<ProductDisplayDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var baseQuery = _productService.GetAll().Include(x => x.CreatedByUser).AsQueryable();

            if (!string.IsNullOrEmpty(request.CreatedByUserId))
            {
                return baseQuery.Where(x => x.CreatedByUserId == request.CreatedByUserId)
                    .Adapt<List<ProductDisplayDto>>();
            }

            return baseQuery.Adapt<List<ProductDisplayDto>>();
        }
    }
}
