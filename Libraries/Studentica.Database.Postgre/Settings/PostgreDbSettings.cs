using Studentica.Services.Common;

namespace Studentica.Database.Postgre.Settings
{
    public class PostgreDbSettings : ISettings
    {
        private readonly string? _connectionString;

        public string Host { get; init; } = "localhost";
        public int Port { get; init; } = 5432;
        public string Database { get; init; } = null!;
        public string UserName { get; init; } = null!;
        public string Password { get; init; } = null!;

        public string ConnectionString
        {
            get =>
                string.IsNullOrWhiteSpace(_connectionString)
                    ? $"Host={Host};Port={Port};Database={Database};Username={UserName};Password={Password}"
                    : _connectionString;
            init => _connectionString = value;
        }
    }
}
