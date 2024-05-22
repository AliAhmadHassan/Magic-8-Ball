using Magic8Ball.Domain.Common;

namespace Magic8Ball.Infra.Data.Common
{
    public class PagedList<TEntity> : IPagedList<TEntity> where TEntity : Domain.Entities.Base.IEntity
    {
        public PagedList(int pageNumber, int pageSize, int totalItemCount, IEnumerable<TEntity> items)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            Items = items;
        }

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalItemCount { get; private set; }
        public IEnumerable<TEntity> Items { get; private set; }
    }
}
