using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Domain.Entities.Base
{
    public abstract class EntityRootBase : EntityBase, IAggregateRoot
    {
        public HashSet<EventBase> DomainEvents { get; private set; }

        public void AddDomainEvent(EventBase eventItem)
        {
            DomainEvents ??= new HashSet<EventBase>();
            DomainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(EventBase eventItem)
        {
            DomainEvents?.Remove(eventItem);
        }
    }
}
