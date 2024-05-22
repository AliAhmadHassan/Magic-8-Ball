using Magic8Ball.Domain.Entities.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Application.Commands
{
    public interface ICreateCommand : ICommand, ITxRequest
    {
    }
}
