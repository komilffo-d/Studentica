using Studentica.Common.DTOs.Request;
using Studentica.Common.DTOs.Requests.Request;

namespace Studentica.Api.Request
{
    public interface IRequestApi<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        Task<RequestDto<T>> Get(T requestId);

        Task<IReadOnlyCollection<RequestDto<T>>> GetAll(int count = int.MaxValue);

        Task<RequestDto<T>> Create(RequestCreateRequest requestCreateRequest);

        Task<RequestDto<T>> UpdateStatus(RequestUpdateStatusRequest<T> requestUpdateStatusRequest);
    }
}
