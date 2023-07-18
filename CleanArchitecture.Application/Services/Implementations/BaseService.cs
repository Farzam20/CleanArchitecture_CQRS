using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Application.Services.Interfaces;
using CleanArchitecture.Domain.Entities;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Services.Implementations
{
    public class BaseService<TEntity> : IBaseService<TEntity>
        where TEntity : class, IBaseEntity
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        #region Async Method
        public virtual async ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _repository.GetByIdAsync(cancellationToken, ids);
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            await _repository.AddAsync(entity, cancellationToken, saveNow).ConfigureAwait(false);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            await _repository.AddRangeAsync(entities, cancellationToken, saveNow).ConfigureAwait(false);
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            await _repository.UpdateAsync(entity, cancellationToken, saveNow);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            await _repository.UpdateRangeAsync(entities, cancellationToken, saveNow);
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            await _repository.DeleteAsync(entity, cancellationToken, saveNow);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            await _repository.DeleteRangeAsync(entities, cancellationToken, saveNow);
        }
        #endregion

        #region Sync Methods
        public virtual TEntity GetById(params object[] ids)
        {
            return _repository.GetById(ids);
        }

        public virtual void Add(TEntity entity, bool saveNow = true)
        {
            _repository.Add(entity, saveNow);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            _repository.AddRange(entities, saveNow);
        }

        public virtual void Update(TEntity entity, bool saveNow = true)
        {
            _repository.Update(entity, saveNow);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            _repository.UpdateRange(entities, saveNow);
        }

        public virtual void Delete(TEntity entity, bool saveNow = true)
        {
            _repository.Delete(entity, saveNow);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            _repository.DeleteRange(entities, saveNow);
        }

        #endregion

        public IQueryable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return _repository.TableNoTracking;
            return _repository.TableNoTracking.Where(predicate);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return _repository.Table;
            return _repository.Table.Where(predicate);
        }
    }
}
