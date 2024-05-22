using Magic8Ball.Domain.Entities;
using Magic8Ball.Domain.Repositories;
using Magic8Ball.Infra.Data.Repositories.Base;
using Microsoft.Extensions.Configuration;

namespace Magic8Ball.Infra.Data.Repositories
{
    public class LogRepository : RepositoryBase<DefaultDbContext, Log>, ILogRepository
    {
        public LogRepository(DefaultDbContext context, IConfiguration configuration) : base(context, configuration) { }
    }
}
