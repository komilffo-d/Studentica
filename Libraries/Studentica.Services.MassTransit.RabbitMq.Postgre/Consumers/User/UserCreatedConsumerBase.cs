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
    public abstract class UserCreatedConsumerBase<TEntity, TType> : IConsumer<UserCreated<TType>>
        where TEntity : IUserPostgreBase<Guid>, new()
        where TType : struct, IEquatable<TType>, IComparable<TType>
    {
        private readonly IUserRepository<Guid> _repository;

        protected UserCreatedConsumerBase(IUserRepository<Guid> repository)
        {
            _repository = repository;
        }

        public virtual async Task Consume(ConsumeContext<UserCreated<TType>> context)
        {
            
        }
    }
}
