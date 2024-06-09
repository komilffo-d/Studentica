using MassTransit;
using Studentica.Database.Postgre.Models;
using Studentica.Services.MassTransit.Contracts.User;

namespace Studentica.Services.MassTransit.RabbitMq.Postgre.Publishers
{
    public static class UserPublishHelper
    {
        public static async Task PublishAlbumCreated<T>(this IPublishEndpoint publishEndpoint, IUserPostgreBase<T> user, string UserName, string Password, Guid RequestId) where T : struct, IEquatable<T>, IComparable<T> =>
                await publishEndpoint.Publish(new UserCreated(UserName, Password, user.Role, RequestId));
    }
}
