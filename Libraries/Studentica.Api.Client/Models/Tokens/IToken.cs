using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentica.Api.Client.Models.Tokens
{
    public delegate void TokenChangedDelegate(IToken token);

    public interface IToken
    {
        event TokenChangedDelegate Changed; 
    }
}
