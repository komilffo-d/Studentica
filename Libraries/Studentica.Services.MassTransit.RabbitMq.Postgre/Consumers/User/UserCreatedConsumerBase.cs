using MassTransit;
using Studentica.Database.Common.Repository;
using Studentica.Database.Postgre.Models;
using Studentica.Infrastructure.Database.Repository.User;
using Studentica.Services.MassTransit.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentica.Services.MassTransit.RabbitMq.Postgre.Consumers.User
{
    public abstract class UserCreatedConsumerBase : IConsumer<UserCreated<Guid>>
    {
        private readonly IUserRepository<Guid> _userRepository;

        protected UserCreatedConsumerBase(IUserRepository<Guid> userRepository)
        {
            _userRepository = userRepository;
        }

        public virtual async Task Consume(ConsumeContext<UserCreated<Guid>> context)
        {
            
        }
    }
}
