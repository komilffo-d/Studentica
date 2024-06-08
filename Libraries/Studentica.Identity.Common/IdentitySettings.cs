using Studentica.Services.Common;

namespace Studentica.Identity.Common
{
    public class IdentitySettings : ISettings
    {
        public string Key { get; init; } = null!;
    }
}
