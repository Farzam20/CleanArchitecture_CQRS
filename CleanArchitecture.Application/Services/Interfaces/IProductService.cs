using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Services.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        Task<bool> ProductIsUnique(Product product);
    }
}
