using CleanArchitecture.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Application.CQRS.ProductFiles.Commands
{
    public class CreateProductCommand : IRequest<HandlerResponse<ProductDisplayDto>>
    {
        public ProductCreateDto Product { get; }
        public string CurrentUserId { get; }

        public CreateProductCommand(ProductCreateDto product, string currentUserId)
        {
            Product = product;
            CurrentUserId = currentUserId;
        }
    }
}