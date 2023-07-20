using MediatR;
using CleanArchitecture.Application.Dtos;

namespace CleanArchitecture.Application.CQRS.ProductFiles.Queries
{
    public class GetAllProductsQuery : IRequest<HandlerResponse<List<ProductDisplayDto>>>
    {
        public string? CreatedByUserId { get; }

        public GetAllProductsQuery(string? createdByUserId)
        {
            CreatedByUserId = createdByUserId;
        }
    }
}
