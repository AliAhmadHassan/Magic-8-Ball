using Magic8Ball.Domain.Entities;
using Magic8Ball.Domain.Repositories.Base;


namespace Magic8Ball.Domain.Repositories
{
    public interface IAnswerRepository: IRepositoryBase<Answer>
    {
        Task<Answer> GetRandomAsync(CancellationToken cancellationToken = default);
    }
}
