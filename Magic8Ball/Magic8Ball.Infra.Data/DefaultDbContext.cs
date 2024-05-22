using Magic8Ball.Domain.Entities;
using Magic8Ball.Infra.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Magic8Ball.Infra.Data
{
    public class DefaultDbContext : DbContext
    {
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Log> Log { get; set; }

        public DefaultDbContext()
        {

        }

        public DefaultDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new LogConfiguration());
        }
    }
}
