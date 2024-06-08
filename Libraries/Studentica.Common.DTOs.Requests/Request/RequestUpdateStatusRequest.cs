using System.ComponentModel.DataAnnotations;
using Studentica.Common.Enums;

namespace Studentica.Common.DTOs.Requests.Request
{
    public record RequestUpdateStatusRequest<T>(T Id,[Required] StatusRequest StatusRequest);
}
