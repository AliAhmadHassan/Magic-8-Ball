using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Magic8Ball.Application.Commands
{
    public interface IQuery : IRequest<ActionResult>
    {
    }
}
