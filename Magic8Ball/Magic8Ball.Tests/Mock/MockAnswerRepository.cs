using Magic8Ball.Domain.Common;
using Magic8Ball.Domain.Entities;
using Magic8Ball.Domain.Entities.Base;
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
    public class MockAnswerRepository : IAnswerRepository
    {
        List<Answer> answerList = AnswerFaker.Gemerate(100).ToList();
        public MockAnswerRepository() { }

        public Task<Answer> AddAsync(Answer entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            answerList.Add(entity);
            return Task.FromResult(entity);
        }

        public Task AddAsync(IEnumerable<Answer> objs, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            answerList.AddRange(objs);
            return Task.FromResult(objs);
        }

        public ValueTask<long> CountAsync(IEnumerable<Func<Answer, bool>> where, CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult<long>(answerList.Count());
        }

        public void Dispose()
        {
            
        }

        public Task<bool> ExistsAsync(IEnumerable<Func<Answer, bool>> where, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(answerList.Any());
        }

        public Task<IEnumerable<Answer>> GetAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IEnumerable<Answer>>(answerList);
        }

        public Task<Answer?> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<Answer?>(answerList.Where(c=>c.Id.Equals(id)).FirstOrDefault());
        }

        public Task<IEnumerable<Answer>> GetAsync(IEnumerable<Func<Answer, bool>> where, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IEnumerable<Answer>>(answerList);
        }

        public Task<IPagedList<Answer>> GetAsync<TKey>(IEnumerable<Func<Answer, bool>> where, IEnumerable<Func<Answer, TKey>> order, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IPagedList<Answer>>(new PagedList<Answer>(1, 1, answerList.Count(), answerList));
        }

        public Task<IPagedList<Answer>> GetAsync<TKey>(IEnumerable<Func<Answer, TKey>> order, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IPagedList<Answer>>(new PagedList<Answer>(1, 1, answerList.Count(), answerList));
        }

        public Task<Answer> GetRandomAsync(CancellationToken cancellationToken = default)
        {
            Random rnd = new Random();
            

            return Task.FromResult<Answer?>(answerList.Skip(rnd.Next(answerList.Count())).Take(1).FirstOrDefault());
        }

        public ValueTask RemoveAsync(int id, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask RemoveAsync(IEnumerable<int> ids, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Answer> UpdateAsync(Answer obj, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
