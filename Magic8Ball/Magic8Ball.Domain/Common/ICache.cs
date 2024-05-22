using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Domain.Common
{
    public interface ICache<TEntity> where TEntity : Entities.Base.IEntity
    {
        Task<TEntity?> GetAsync(string key);
        Task AddAsync(string key, TEntity obj, DateTime expirationDate);
        void Remove(string key);
    }
}
