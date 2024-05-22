namespace Magic8Ball.Application.Exceptions
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException(string message) : base(message, 500)
        {

        }
    }
}
