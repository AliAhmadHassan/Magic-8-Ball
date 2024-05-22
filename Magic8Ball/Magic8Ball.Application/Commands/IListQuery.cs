using Magic8Ball.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Application.Commands
{
    public interface IListQuery<TEntity> : IQuery where TEntity : IEntity
    {
        public List<string> Includes { get; init; }
        public List<Func<TEntity, bool>> Filters { get; init; }
        public List<string> Sorts { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
    }
}
