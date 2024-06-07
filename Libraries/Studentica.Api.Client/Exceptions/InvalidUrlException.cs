namespace Studentica.Api.Exceptions
{
    public class InvalidUrlException : ExceptionBase
    {
        private const string DefaultMessage = "Неверный URL-адрес.";

        public InvalidUrlException(string message = DefaultMessage) : base(message == "" ? DefaultMessage : message)
        {
        }
    }
}
