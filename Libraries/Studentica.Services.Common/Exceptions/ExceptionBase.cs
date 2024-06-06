using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Studentica.Services.Common.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        protected ExceptionBase(HttpStatusCode code, string message) : base(message)
        {
            StatusCode = code;
        }
    }
}
