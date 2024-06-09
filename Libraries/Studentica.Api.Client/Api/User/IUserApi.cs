using Studentica.Common.DTOs.Requests.User;
using Studentica.Common.DTOs.User;

namespace Studentica.Api.User
{
    public interface IUserApi<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        Task<UserDto<T>> Get(T userId);

        Task<IReadOnlyCollection<UserDto<T>>> GetAll(int count = int.MaxValue, string? searchQuery = null);

        Task<UserDto<T>> Create(UserCreateRequest userCreateRequest);
    }
}
