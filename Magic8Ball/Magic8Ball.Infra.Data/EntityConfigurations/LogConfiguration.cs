using Magic8Ball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magic8Ball.Infra.Data.EntityConfigurations
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable(nameof(Log));
            builder
               .Property(x => x.Id)
               .ValueGeneratedOnAdd()
               .IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.UpdatedAt).HasColumnType("datetime");
            builder.Property(x => x.IsActive).HasColumnType("bit").IsRequired();
        }
    }
}