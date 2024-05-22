using Magic8Ball.Domain.Common;
using Magic8Ball.Domain.Entities.Base;
using System.Linq.Expressions;

namespace Magic8Ball.Domain.Repositories.Base
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : EntityBase, IEntity
    {
        Task<TEntity> AddAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default);
        Task AddAsync(IEnumerable<TEntity> objs, bool autoSave = true, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity obj, bool autoSave = true, CancellationToken cancellationToken = default);
        ValueTask RemoveAsync(int id, bool autoSave = true, CancellationToken cancellationToken = default);
        ValueTask RemoveAsync(IEnumerable<int> ids, bool autoSave = true, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(IEnumerable<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default);
        Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAsync(IEnumerable<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);
        Task<IPagedList<TEntity>> GetAsync<TKey>(IEnumerable<Func<TEntity, bool>> where, IEnumerable<Func<TEntity, TKey>> order, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<IPagedList<TEntity>> GetAsync<TKey>(IEnumerable<Func<TEntity, TKey>> order, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        ValueTask<long> CountAsync(IEnumerable<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);
    }
}
