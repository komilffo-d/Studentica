using Microsoft.EntityFrameworkCore;
using Studentica.Common.DTOs.Converters;
using Studentica.Common.DTOs.Request;
using Studentica.Common.DTOs.Requests.Request;
using Studentica.Common.DTOs.Requests.User;
using Studentica.Common.DTOs.User;
using Studentica.Common.Enums;
using Studentica.Database.Postgre.Models;
using Studentica.Identity.Common;
using Studentica.Infrastructure.Database.Repository.User;
using Studentica.Services.Common.Exceptions;
using System.Linq.Expressions;

namespace Studentica.Api.Services
{
    public interface IUserService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        Task<UserDto<T>> Get(T userId);
        Task<IReadOnlyCollection<UserDto<T>>> GetAllAsync(int count = int.MaxValue, string? searchQuery = null);
        Task<UserDto<T>> Create(UserCreateRequest request, HttpContext context);
    }
    public class UserService<T> : IUserService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        private readonly IUserRepository<T> _userRepository;
        public UserService(IUserRepository<T> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto<T>> Get(T userId)
        {
            var user = await _userRepository.GetAsync(userId) ?? throw new NotFoundException(ExceptionMessages.RequestNotFound);

            return user.AsDto();
        }
        public async Task<IReadOnlyCollection<UserDto<T>>> GetAllAsync(int count = int.MaxValue, string? searchQuery = null)
        {
            Expression<Func<UserPostgreBase<T>, bool>> filter = u => true;
            if (searchQuery is not null)
                filter = u => u.LastName.ToLower().Contains(searchQuery.ToLower()) ||
                            u.FirstName.ToLower().Contains(searchQuery.ToLower()) ||
                            u.MiddleName.ToLower().Contains(searchQuery.ToLower());

            var usersQuery = _userRepository
                .GetAll(filter, p => p.Id, 0, count)
                .Select(p => p.AsDto());

            return await usersQuery.ToListAsync();
        }

        public async Task<UserDto<T>> Create(UserCreateRequest request, HttpContext context)
        {
            var requestEntity = new UserPostgreBase<T>()
            {
                LastName = request.LastName,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                NumberCourse= request.NumberCourse,
                Group=request.Group,
                Major=request.Major,
                NameUniversity=request.NameUniversity,
                CreatedDate=DateTimeOffset.Now,
                UpdatedDate = DateTimeOffset.Now,
                IsActive=true,
                Role= request.Role,
                Email = request.Email,
            };
            await _userRepository.CreateAsync(requestEntity);

            return requestEntity.AsDto();
        }
    }
}
