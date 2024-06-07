namespace Studentica.Api.Client.Models.Tokens
{
    public interface IJwtTokenModel : IToken
    {
        void ChangeToken(string jwtAccessToken);
        string AccessToken { get; }
    }
}
