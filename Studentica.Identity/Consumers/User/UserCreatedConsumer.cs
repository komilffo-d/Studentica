using MassTransit;
using Studentica.Database.Postgre.Models;
using Studentica.Identity.Common.Models;
using Studentica.Infrastructure.Database.Repository.Identity;
using Studentica.Infrastructure.Database.Repository.User;
using Studentica.Services.MassTransit.Contracts.User;
using Studentica.Services.MassTransit.RabbitMq.Postgre.Consumers.User;

namespace Studentica.Identity.Consumers.User
{
    public class UserCreatedConsumer<TEntity,TType> : UserCreatedConsumerBase<TEntity,TType>
        where TEntity : IUserPostgreBase<Guid>, new()
        where TType : struct, IEquatable<TType>, IComparable<TType>
    {
        private readonly IIdentityRepository _identityRepository;
        public UserCreatedConsumer(IUserRepository<Guid> userRepository, IIdentityRepository identityRepository) : base(userRepository)
        {
            _identityRepository = identityRepository;
        }

        public override async Task Consume(ConsumeContext<UserCreated<TType>> context)
        {
            await base.Consume(context);

            var message = context.Message;

            await _identityRepository.Register(new RegisterModel() { Username = message.UserName, Password = message.Password, Role = message.Role });
        }
    }
}
