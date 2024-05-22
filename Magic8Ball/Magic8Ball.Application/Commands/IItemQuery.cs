using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Application.Commands
{
    public interface IItemQuery<TId> : IQuery
        where TId : struct
    {
        public List<string> Includes { get; init; }
        public TId Id { get; init; }
    }
}
