namespace Studentica.Api.Exceptions
{
    public class NullTokenException : ExceptionBase
    {
        private const string DefaultMessage = "Токен пустой";

        public NullTokenException(string message = DefaultMessage) : base(message == "" ? DefaultMessage : message)
        {
        }
    }
}
