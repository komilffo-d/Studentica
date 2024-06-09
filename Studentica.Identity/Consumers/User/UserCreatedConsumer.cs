using MassTransit;
using Studentica.Database.Postgre.Models;
using Studentica.Identity.Common.Models;
using Studentica.Infrastructure.Database.Repository.Identity;
using Studentica.Infrastructure.Database.Repository.User;
using Studentica.Services.MassTransit.Contracts.User;
using Studentica.Services.MassTransit.RabbitMq.Postgre.Consumers.User;

namespace Studentica.Identity.Consumers.User
{
    public class UserCreatedConsumer : UserCreatedConsumerBase
    {
        private readonly IIdentityRepository _identityRepository;
        public UserCreatedConsumer(IIdentityRepository identityRepository, IUserRepository<Guid>? userRepository = null) : base(userRepository)
        {
            _identityRepository = identityRepository;
        }

        public override async Task Consume(ConsumeContext<UserCreated<Guid>> context)
        {
            await base.Consume(context);

            var message = context.Message;

            await _identityRepository.Register(new RegisterModel() { Username = message.UserName, Password = message.Password, Role = message.Role });
        }
    }
}
