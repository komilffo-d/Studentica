using System.Text.Json.Serialization;

namespace Studentica.Identity.Data
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRoles : sbyte
    {
        Student,
        Teacher,
        Admin
    }
}
