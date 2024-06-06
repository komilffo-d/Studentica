using System.Net;

namespace Studentica.Services.Common.Exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {
        }
    }
}
