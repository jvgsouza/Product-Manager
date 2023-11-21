namespace Usuario.Application.Exceptions
{
    public class APIException : Exception
    {
        public APIException(string message) : base(message)
        {
        }
    }
}
