using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentica.Api.Client
{
    public sealed class ApiClient<T> : ApiClientBase<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        public ApiClient(string gatewayPath, string accessToken) :
            base(gatewayPath, accessToken)
        {
        }
    }
}
