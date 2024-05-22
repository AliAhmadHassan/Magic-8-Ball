using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Domain.Common
{
    public interface IPagedList<TEntity> where TEntity : Entities.Base.IEntity
    {
        int PageNumber { get; }
        int PageSize { get; }
        int TotalItemCount { get; }

        IEnumerable<TEntity> Items { get; }
    }
}
