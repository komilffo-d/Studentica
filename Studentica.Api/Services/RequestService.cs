using Microsoft.EntityFrameworkCore;
using Studentica.Common.DTOs.Converters;
using Studentica.Common.DTOs.Request;
using Studentica.Common.DTOs.Requests.Request;
using Studentica.Common.Enums;
using Studentica.Database.Postgre.Models;
using Studentica.Infrastructure.Database.Repository.Request;
using Studentica.Services.Common.Exceptions;
using System.Linq.Expressions;

namespace Studentica.Api.Services
{
    public interface IRequestService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        Task<RequestDto<T>> Get(T requestId);
        Task<IReadOnlyCollection<RequestDto<T>>> GetAllAsync(int count = int.MaxValue, Expression<Func<RequestPostgreBase<T>, bool>>? filter = null);
        Task<RequestDto<T>> Create(RequestCreateRequest request, HttpContext context);
        Task<RequestDto<T>> UpdateStatus(RequestUpdateStatusRequest<T> request, HttpContext context);
    }
    public class RequestService<T> : IRequestService<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        private readonly IRequestRepository<T> _requestRepository;
        public RequestService(IRequestRepository<T> requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<RequestDto<T>> Get(T requestId)
        {
            var request = await _requestRepository.GetAsync(requestId) ?? throw new NotFoundException(ExceptionMessages.RequestNotFound);

            return request.AsDto();
        }
        public async Task<IReadOnlyCollection<RequestDto<T>>> GetAllAsync(int count = int.MaxValue, Expression<Func<RequestPostgreBase<T>, bool>>? filter=null)
        {
            Expression<Func<RequestPostgreBase<T>, bool>> defaultFilter = p => true;

            var requestsQuery = _requestRepository
                .GetAll(filter ?? defaultFilter, p => p.Id, 0, count)
                .Select(p => p.AsDto());

            return await requestsQuery.ToListAsync();
        }

        public async Task<RequestDto<T>> Create(RequestCreateRequest request, HttpContext context)
        {
            var requestEntity = new RequestPostgreBase<T>()
            {
                LastName = request.LastName,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                Email = request.Email,
                NumberGroup = request.NumberGroup,
                NameUniversity = request.NameUniversity,
                CreatedDate = DateTimeOffset.Now,
                StatusRequest = StatusRequest.INPROCESS
            };
            await _requestRepository.CreateAsync(requestEntity);

            return requestEntity.AsDto();
        }

        public async Task<RequestDto<T>> UpdateStatus(RequestUpdateStatusRequest<T> request, HttpContext context)
        {
            var requestEntity = await _requestRepository.GetAsync(request.Id) ?? throw new NotFoundException(ExceptionMessages.RequestNotFound);

            requestEntity.StatusRequest = request.StatusRequest;

            await _requestRepository.UpdateAsync(request.Id, requestEntity);

            return requestEntity.AsDto();
        }
    }
}
