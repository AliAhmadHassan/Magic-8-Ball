using Magic8Ball.Domain.Common;

namespace Magic8Ball.Infra.Data.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DefaultDbContext _context;

        public UnitOfWork(DefaultDbContext context)
        {
            _context = context;
        }

        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
