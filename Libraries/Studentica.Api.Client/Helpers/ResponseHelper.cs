using Newtonsoft.Json;
using RestSharp;
using Studentica.Api.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentica.Api.Helpers
{
    public static class ResponseHelper
    {
        public static T Deserialize<T>(this RestResponse response)
        {
            if (response.Content == null)
                throw new ServerUnavailableException("Problem with server");
            return JsonConvert.DeserializeObject<T>(response.Content!)
                   ?? throw new ServerUnavailableException("Problem with server"); ;
        }
    }
}
