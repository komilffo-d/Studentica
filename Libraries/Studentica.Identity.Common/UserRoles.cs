using System.Text.Json.Serialization;

namespace Studentica.Identity.Common
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRoles : sbyte
    {
        Student,
        Teacher,
        Admin
    }
}
