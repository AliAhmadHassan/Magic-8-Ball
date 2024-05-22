using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Domain.Entities.Base
{
    public interface IDomainEvent : INotification
    {
        DateTime CreatedAt { get; }
        IDictionary<string, object> MetaData { get; }
    }

    public interface IDomainEventContext
    {
        IEnumerable<EventBase> GetDomainEvents();
    }

    public abstract class EventBase : IDomainEvent
    {
        public string EventType { get { return GetType().FullName; } }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        public string CorrelationId { get; init; }
        public IDictionary<string, object> MetaData { get; } = new Dictionary<string, object>();
        public abstract void Flatten();
    }

    public class EventWrapper : INotification
    {
        public EventWrapper(IDomainEvent @event)
        {
            Event = @event;
        }

        public IDomainEvent Event { get; }
    }

    public class DaprPubSubNameAttribute : Attribute
    {
        public string PubSubName { get; set; } = "pubsub";
    }
}
