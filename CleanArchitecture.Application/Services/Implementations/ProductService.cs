using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Application.Services.Interfaces;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Services.Implementations
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IBaseRepository<Product> _repository;

        public ProductService(IBaseRepository<Product> repository) : base(repository)
        {
            this._repository = repository;
        }

        public override async Task<Product> AddAsync(Product entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            var result = await base.AddAsync(entity, cancellationToken, saveNow);
            await _repository.LoadReferenceAsync(result, x => x.CreatedByUser, cancellationToken);
            return result;
        }

        public override async ValueTask<Product> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            var result = await base.GetByIdAsync(cancellationToken, ids);
            
            if (result != null)
                await _repository.LoadReferenceAsync(result, x => x.CreatedByUser, cancellationToken);

            return result;
        }

        public override async Task<Product> UpdateAsync(Product entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            var result = await base.UpdateAsync(entity, cancellationToken, saveNow);
            await _repository.LoadReferenceAsync(result, x => x.CreatedByUser, cancellationToken);
            return result;
        }

        public async Task<bool> ProductIsUnique(Product product)
        {
            return !(await _repository.TableNoTracking.AnyAsync(x => x.ProduceDate == product.ProduceDate && x.ManufactureEmail == product.ManufactureEmail && x.Id != product.Id));
        }
    }
}
