using System.ComponentModel.DataAnnotations;

namespace Magic8Ball.Domain.Entities.Base
{
    public abstract class EntityBase:IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (EntityBase)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
