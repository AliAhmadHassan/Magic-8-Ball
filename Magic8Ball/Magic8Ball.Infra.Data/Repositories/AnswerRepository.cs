using Magic8Ball.Domain.Entities;
using Magic8Ball.Domain.Repositories;
using Magic8Ball.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Infra.Data.Repositories
{
    public class AnswerRepository : RepositoryBase<DefaultDbContext, Answer>, IAnswerRepository
    {
        private Random random;

        public AnswerRepository(DefaultDbContext context, IConfiguration configuration) : base(context, configuration)
        {
            random = new Random();
        }

        public async Task<Answer> GetRandomAsync(CancellationToken cancellationToken = default)
        {
            int amount = await _dbSet.Where(_ => _.IsActive).CountAsync(cancellationToken);

            int step = random.Next(amount);

            Answer answer = await _dbSet.Where(_ => _.IsActive).Skip(step).Take(1).FirstAsync(cancellationToken);
            return answer;
        }
    }
}
