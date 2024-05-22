using System;
using System.Collections.Generic;
using System.Linq;

namespace Magic8Ball.Domain.Entities.Base
{
    public interface IAggregateRoot : IEntity
    {
        public HashSet<EventBase> DomainEvents { get; }
    }
}
