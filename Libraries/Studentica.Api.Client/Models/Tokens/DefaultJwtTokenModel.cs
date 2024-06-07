using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentica.Api.Client.Models.Tokens
{
    public class DefaultJwtTokenModel : JwtTokenModelBase
    {
        public DefaultJwtTokenModel(string accessToken) : base(accessToken)
        {
        }
    }
}
