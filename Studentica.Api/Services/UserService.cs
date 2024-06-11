using Microsoft.EntityFrameworkCore;
using Studentica.Common.DTOs.Converters;
using Studentica.Common.DTOs.Requests.User;
using Studentica.Common.DTOs.User;
using Studentica.Common.Enums;
using Studentica.Database.Postgre.Models;
using Studentica.Identity.Common.Models;
using Studentica.Infrastructure.Database.Repository.Request;
using Studentica.Infrastructure.Database.Repository.User;
using Studentica.Services.Common.Exceptions;
using System.Linq.Expressions;

namespace Studentica.Api.Services
{
    public interface IUserService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        Task<UserDto<T>> GetById(T userId);
        Task<UserDto<T>> GetByIdentityId(T identityId);
        Task<IReadOnlyCollection<UserDto<T>>> GetAllAsync(int count = int.MaxValue, string? searchQuery = null);
        Task<UserDto<T>> Create(T identityId, UserCreateRequest request, HttpContext context);
    }
    public class UserService<T> : IUserService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        private readonly IUserRepository<T> _userRepository;
        private readonly IIdentityService<Guid> _identityService;
        private readonly IRequestRepository<Guid> _requestRepository;

        public UserService(IUserRepository<T> userRepository, IIdentityService<Guid> identityService, IRequestRepository<Guid> requestRepository)
        {
            _userRepository = userRepository;
            _identityService = identityService;
            _requestRepository = requestRepository;
        }

        public async Task<UserDto<T>> GetById(T userId)
        {
            var user = await _userRepository.GetAsync(userId) ?? throw new NotFoundException(ExceptionMessages.RequestNotFound);

            return user.AsDto();
        }
        public async Task<UserDto<T>> GetByIdentityId(T identityId)
        {
            var user = await _userRepository.GetAsync(u => u.IdentityId.Equals(identityId.ToString())) ?? throw new NotFoundException(ExceptionMessages.RequestNotFound);

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

        public async Task<UserDto<T>> Create(T identityId, UserCreateRequest request, HttpContext context)
        {
            var identityTuple= await _identityService.Register(new RegisterModel() { Username = request.UserName, Password = request.Password, Role = request.Role });

            var userEntity = new UserPostgreBase<T>()
            {
                LastName = request.LastName,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                NumberCourse = request.NumberCourse,
                Group = request.Group,
                Major = request.Major,
                NameUniversity = request.NameUniversity,
                CreatedDate = DateTimeOffset.Now,
                UpdatedDate = DateTimeOffset.Now,
                IsActive = true,
                Role = request.Role,
                IdentityId = identityTuple.Item1!.Id,
                Email = request.Email,
            };

            await _userRepository.CreateAsync(userEntity);

            var requestEntity = await _requestRepository.GetAsync(request.RequestId);
            
            if (requestEntity!= null)
            {
                requestEntity.StatusRequest = StatusRequest.ACCEPTED;
                await _requestRepository.UpdateAsync(request.RequestId, requestEntity);
            }
                
            return userEntity.AsDto();
        }
    }
}
