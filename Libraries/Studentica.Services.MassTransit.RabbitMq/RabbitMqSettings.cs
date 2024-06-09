using Studentica.Services.Common;

namespace Studentica.Services.MassTransit.RabbitMq
{
    public class RabbitMqSettings : ISettings
    {
        public string Host { get; init; } = null!;
    }
}
