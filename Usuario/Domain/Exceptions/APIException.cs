namespace Usuario.Domain.Exceptions
{
    public class APIException : Exception
    {
        public APIException(string message) : base(message)
        {
        }
    }
}
