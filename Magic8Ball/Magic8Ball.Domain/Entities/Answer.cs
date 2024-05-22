using Magic8Ball.Domain.Enums;

namespace Magic8Ball.Domain.Entities
{
    public class Answer : Base.EntityBase
    {
        public string Message { get; set; } = string.Empty;
        public AnswerType Type { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Message;
            yield return Type;
        }
    }
}
