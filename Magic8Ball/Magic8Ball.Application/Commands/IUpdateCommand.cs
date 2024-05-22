using Magic8Ball.Domain.Entities.Base;

namespace Magic8Ball.Application.Commands
{
    public interface IUpdateCommand<TId> : ICommand, ITxRequest
        where TId : struct
    {
        public TId Id { get; init; }
    }
}
