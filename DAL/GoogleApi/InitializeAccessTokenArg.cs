namespace AmoebaGameMatcherServer.Services
{
    public class InitializeAccessTokenArg
    {
        public string Code;
        public string ClientId;
        public string ClientSecret;
        public string RedirectUri;
    }
}