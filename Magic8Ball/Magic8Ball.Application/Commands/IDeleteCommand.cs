using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Application.Commands
{
    public interface IDeleteCommand<TId> : ICommand
        where TId : struct
    {
        public TId Id { get; init; }
    }
}
