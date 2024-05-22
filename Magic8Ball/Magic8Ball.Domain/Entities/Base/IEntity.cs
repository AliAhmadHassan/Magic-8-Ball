using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Domain.Entities.Base
{
    public interface IEntity
    {
        int Id { get; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }

        bool IsActive { get; }

        bool Equals(object obj);
    }
}
