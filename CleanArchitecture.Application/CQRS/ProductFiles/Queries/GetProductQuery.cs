using MediatR;
using CleanArchitecture.Application.Dtos;

namespace CleanArchitecture.Application.CQRS.ProductFiles.Queries
{
    public class GetProductQuery : IRequest<HandlerResponse<ProductDisplayDto>>
    {
        public int Id { get; }

        public GetProductQuery(int id)
        {
            Id = id;
        }
    }
}
