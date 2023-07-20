using CleanArchitecture.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Application.CQRS.ProductFiles.Commands
{
    public class UpdateProductCommand : IRequest<HandlerResponse<ProductDisplayDto>>
    {
        public string CurrentUserId { get; }
        public ProductCreateDto Product { get; }

        public UpdateProductCommand(ProductCreateDto product, string currentUserId)
        {
            Product = product;
            CurrentUserId = currentUserId;
        }
    }
}
