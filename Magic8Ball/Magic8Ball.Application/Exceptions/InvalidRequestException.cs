namespace Magic8Ball.Application.Exceptions
{
    public class InvalidRequestException : BaseException
    {
        public InvalidRequestException(string message) : base(message, 400)
        {

        }
    }
}
