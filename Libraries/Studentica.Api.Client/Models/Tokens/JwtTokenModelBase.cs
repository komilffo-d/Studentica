namespace Studentica.Api.Client.Models.Tokens
{
    public abstract class JwtTokenModelBase : IJwtTokenModel
    {
        public string AccessToken { get; private set; }

        protected JwtTokenModelBase(string accessToken)
        {
            AccessToken = accessToken;
        }


        public virtual void ChangeToken(string jwtAccessToken)
        {
            AccessToken = jwtAccessToken;
            Changed?.Invoke(this);
        }

        public event TokenChangedDelegate? Changed;
    }
}
