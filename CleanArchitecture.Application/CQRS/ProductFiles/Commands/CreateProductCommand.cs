using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.CQRS.ProductFiles.Commands
{
    public class CreateProductCommand : IRequest<HandlerResponse<ProductDisplayDto>>
    {
        public ProductCreateDto Product { get; }

        public CreateProductCommand(ProductCreateDto product)
        {
            Product = product;
        }
    }
}