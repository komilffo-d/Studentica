namespace Studentica.Identity.Common.Data
{
    public record class TokenCache(string Token, bool IsValidToken = false);
}
