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
            if (!string.IsNullOrEmpty(request.CreatedByUserId))
            {
                return (await _productService.GetAll(x => x.CreatedByUserId == request.CreatedByUserId).Include(x => x.CreatedByUser).ToListAsync(cancellationToken))
                    .Adapt<List<ProductDisplayDto>>();
            }

            return (await _productService.GetAll().Include(x => x.CreatedByUser).ToListAsync(cancellationToken)).Adapt<List<ProductDisplayDto>>();
        }
    }
}
