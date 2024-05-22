namespace Magic8Ball.Application.Exceptions
{
    public class InvalidAnswersException : BaseException
    {
        public InvalidAnswersException(string message) : base(message, 400)
        {
        }
    }
}
