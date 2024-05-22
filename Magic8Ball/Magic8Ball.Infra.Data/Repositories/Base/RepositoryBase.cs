using Magic8Ball.Domain.Common;
using Magic8Ball.Domain.Entities.Base;
using Magic8Ball.Domain.Repositories.Base;
using Magic8Ball.Infra.Data.Common;
using Magic8Ball.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;


namespace Magic8Ball.Infra.Data.Repositories.Base
{
    public abstract class RepositoryBase<TDbContext, TEntity> : IRepositoryBase<TEntity>
        where TEntity : EntityBase, IEntity
        where TDbContext : DbContext
    {
        protected readonly TDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IConfiguration _configuration;

        public RepositoryBase(TDbContext context, IConfiguration configuration)
        {
            this._context = context;
            _dbSet = context.Set<TEntity>();
            _configuration = configuration;
        }

        public async Task<TEntity> AddAsync(TEntity obj, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(obj, cancellationToken);

            if (autoSave)
                await _context.SaveChangesAsync(cancellationToken);

            return obj;
        }


        public async Task AddAsync(IEnumerable<TEntity> objs, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(objs);

            if (autoSave)
                await _context.SaveChangesAsync(cancellationToken);

            return;
        }

        public async ValueTask RemoveAsync(int id, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            TEntity? entity = await GetAsync(id, cancellationToken);
            
            if(entity == null)
                return ;

            entity.IsActive = false;
            await UpdateAsync(entity, autoSave, cancellationToken);
        }

        public async ValueTask RemoveAsync(IEnumerable<int> ids, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            int maxDegreeOfParallelism;

            if (int.TryParse(_configuration["ApplicationSetting:MaxDegreeOfParallelism"], out maxDegreeOfParallelism))
                maxDegreeOfParallelism = 20;

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };

            await Parallel.ForEachAsync(ids, options, async (id, cancellationToken) =>
            {
                TEntity? entity = await GetAsync(id, cancellationToken);
                if(entity != null) { 
                    entity.IsActive = false;
                    await UpdateAsync(entity, autoSave, cancellationToken);
                }
            });
        }

        public async Task<TEntity> UpdateAsync(TEntity obj, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            obj.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

            var entry = _context.Update(obj);
            entry.State = EntityState.Modified;

            if (autoSave)
            {
                await _context.SaveChangesAsync(cancellationToken);
            }

            return await Task.FromResult(entry.Entity);
        }

        public Task<bool> ExistsAsync(IEnumerable<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
            => _dbSet.AnyAsync(where, cancellationToken);

        public async Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default)
            => await _dbSet.Where(x=>x.IsActive).ToListAsync(cancellationToken);

        public async Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default)
            => await _dbSet.FindAsync(id, cancellationToken);

        public async Task<IEnumerable<TEntity>> GetAsync(IEnumerable<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
            => await _dbSet.AsNoTracking().Where(where).ToListAsync();

        public Task<IPagedList<TEntity>> GetAsync<TKey>(IEnumerable<Func<TEntity, bool>> where, IEnumerable<Func<TEntity, TKey>> order, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return _dbSet
                .Where(where)
                .OrderBy(order)
                .ToPagedListAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<IPagedList<TEntity>> GetAsync<TKey>(IEnumerable<Func<TEntity, TKey>> order, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var items = await _context.Set<TEntity>()
               .ToArrayAsync(cancellationToken);

            return new PagedList<TEntity>(pageNumber, pageSize, 10, items);
        }

        public IQueryable<TEntity> Query
        {
            get { return _context.Set<TEntity>(); }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }



        public async ValueTask<long> CountAsync(IEnumerable<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(where)
                .CountAsync(cancellationToken);
        }
    }
}
