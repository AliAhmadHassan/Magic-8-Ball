using Magic8Ball.Domain.Common;
using Magic8Ball.Domain.Entities;
using Magic8Ball.Domain.Repositories;
using Magic8Ball.Infra.Data.Common;
using Magic8Ball.Tests.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Tests.Mock
{
    public class MockLogRepository : ILogRepository
    {
        public Task<Log> AddAsync(Log entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<Log?>(LogFaker.Gemerate(1).First());// throw new NotImplementedException();
        }

        public Task AddAsync(IEnumerable<Log> objs, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IEnumerable<Log>>(LogFaker.Gemerate(10));// throw new NotImplementedException();
        }

        public ValueTask<long> CountAsync(IEnumerable<Func<Log, bool>> where, CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult<long>(20);// throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(IEnumerable<Func<Log, bool>> where, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<bool>(true);// throw new NotImplementedException();
        }

        public Task<IEnumerable<Log>> GetAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IEnumerable<Log>>(LogFaker.Gemerate(10));// throw new NotImplementedException();
        }

        public Task<Log?> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<Log?>(LogFaker.Gemerate(1).First());// throw new NotImplementedException();
        }

        public Task<IEnumerable<Log>> GetAsync(IEnumerable<Func<Log, bool>> where, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IEnumerable<Log>>(LogFaker.Gemerate(10));// throw new NotImplementedException();
        }

        public Task<IPagedList<Log>> GetAsync<TKey>(IEnumerable<Func<Log, bool>> where, IEnumerable<Func<Log, TKey>> order, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IPagedList<Log>>(new PagedList<Log>(1, 1, 10, LogFaker.Gemerate(10)));// throw new NotImplementedException();
        }

        public Task<IPagedList<Log>> GetAsync<TKey>(IEnumerable<Func<Log, TKey>> order, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IPagedList<Log>>(new PagedList<Log>(1, 1, 10, LogFaker.Gemerate(10)));// throw new NotImplementedException();
        }

        public ValueTask RemoveAsync(int id, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask RemoveAsync(IEnumerable<int> ids, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Log> UpdateAsync(Log obj, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
