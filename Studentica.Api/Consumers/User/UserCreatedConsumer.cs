using MassTransit;
using Studentica.Common.Enums;
using Studentica.Common.Interfaces;
using Studentica.Database.Postgre.Models;
using Studentica.Infrastructure.Database.Repository.Request;
using Studentica.Infrastructure.Database.Repository.User;
using Studentica.Services.MassTransit.Contracts.User;
using Studentica.Services.MassTransit.RabbitMq.Postgre.Consumers.User;

namespace Studentica.Api.Consumers.User
{
    public class UserCreatedConsumer: UserCreatedConsumerBase
    {
    
        private readonly IRequestRepository<Guid> _requestRepository;
        public UserCreatedConsumer(IUserRepository<Guid> userRepository, IRequestRepository<Guid> requestRepository) : base(userRepository)
        {
            _requestRepository = requestRepository;
        }

        public override async Task Consume(ConsumeContext<UserCreated> context)
        {
            await base.Consume(context);

            var message = context.Message;

            var request = await _requestRepository.GetAsync(message.RequestId);

            if (request == null)
                return;

            request.StatusRequest = StatusRequest.ACCEPTED;

            await _requestRepository.UpdateAsync(message.RequestId, request);

        }
    }
}
