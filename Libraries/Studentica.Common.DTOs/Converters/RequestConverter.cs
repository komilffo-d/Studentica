using Studentica.Common.DTOs.Request;
using Studentica.Services.Models;

namespace Studentica.Common.DTOs.Converters
{
    public static class RequestConverter
    {

        public static RequestDto<T> AsDto<T>(this IRequestBase<T> request) where T : struct, IEquatable<T>, IComparable<T>
        {
            return new(request.Id, request.LastName, request.FirstName, request.MiddleName, request.Email, request.NumberGroup, request.NameUniversity, request.NumberRequest, request.CreatedDate);
        }
    }
}
