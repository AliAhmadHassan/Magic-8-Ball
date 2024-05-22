using Magic8Ball.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magic8Ball.Infra.Data.EntityConfigurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable(nameof(Answer));
            builder
               .Property(x => x.Id)
               .ValueGeneratedOnAdd()
               .IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.UpdatedAt).HasColumnType("datetime");
            builder.Property(x => x.IsActive).HasColumnType("bit").IsRequired();

            builder.Property(x => x.Message).HasColumnType("varchar(800)").HasMaxLength(800).IsRequired();

            DataSeeding(builder);
        }

        private void DataSeeding(EntityTypeBuilder<Answer> builder)
        {
            builder.HasData(
                new Answer()
                {
                    Id = 1,
                    Message = "It is certain.",
                    Type = Domain.Enums.AnswerType.AffirmativeAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 2,
                    Message = "It is decidedly so.",
                    Type = Domain.Enums.AnswerType.AffirmativeAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 3,
                    Message = "Without a doubt.",
                    Type = Domain.Enums.AnswerType.AffirmativeAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 4,
                    Message = "Yes definitely.",
                    Type = Domain.Enums.AnswerType.AffirmativeAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 5,
                    Message = "You may rely on it.",
                    Type = Domain.Enums.AnswerType.AffirmativeAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 6,
                    Message = "As I see it, yes.",
                    Type = Domain.Enums.AnswerType.AffirmativeAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 7,
                    Message = "Most likely.",
                    Type = Domain.Enums.AnswerType.AffirmativeAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 8,
                    Message = "Outlook good.",
                    Type = Domain.Enums.AnswerType.AffirmativeAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 9,
                    Message = "Yes.",
                    Type = Domain.Enums.AnswerType.AffirmativeAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 10,
                    Message = "Signs point to yes.",
                    Type = Domain.Enums.AnswerType.AffirmativeAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 11,
                    Message = "Reply hazy, try again.",
                    Type = Domain.Enums.AnswerType.NonCommittalAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 12,
                    Message = "Ask again later.",
                    Type = Domain.Enums.AnswerType.NonCommittalAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 13,
                    Message = "Better not tell you now.",
                    Type = Domain.Enums.AnswerType.NonCommittalAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 14,
                    Message = "Cannot predict now.",
                    Type = Domain.Enums.AnswerType.NonCommittalAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 15,
                    Message = "Concentrate and ask again.",
                    Type = Domain.Enums.AnswerType.NonCommittalAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 16,
                    Message = "Don't count on it.",
                    Type = Domain.Enums.AnswerType.NegativeAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 17,
                    Message = "My reply is no.",
                    Type = Domain.Enums.AnswerType.NegativeAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 18,
                    Message = "My sources say no.",
                    Type = Domain.Enums.AnswerType.NegativeAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 19,
                    Message = "Outlook not so good.",
                    Type = Domain.Enums.AnswerType.NegativeAnswers,
                    IsActive = true,
                },
                new Answer()
                {
                    Id = 20,
                    Message = "Very doubtful.",
                    Type = Domain.Enums.AnswerType.NegativeAnswers,
                    IsActive = true,
                });
        }
    }
}
