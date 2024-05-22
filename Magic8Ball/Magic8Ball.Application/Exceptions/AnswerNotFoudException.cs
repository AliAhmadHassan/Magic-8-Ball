namespace Magic8Ball.Application.Exceptions
{
    public class AnswerNotFoudException : BaseException
    {
        public AnswerNotFoudException(string message) : base(message, 404)
        {
        }
    }
}