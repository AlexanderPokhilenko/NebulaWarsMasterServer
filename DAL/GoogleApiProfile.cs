using System;
using AmoebaGameMatcherServer.Services;

namespace DataLayer
{
    public abstract class GoogleApiProfile
    {
        /// <summary>
        /// Код можно использовать только один раз.
        /// </summary>
        public abstract string Code { get; }
        public abstract string ClientId{ get; }
        public abstract string ClientSecret{ get; }
        public abstract string RedirectUri{ get; }
        public abstract string PackageName{ get; }
        public abstract string GoogleApiData{ get; }

        public GoogleApiProfile()
        {
            if (string.IsNullOrEmpty(ClientId))
            {
                throw new Exception($"{nameof(ClientId)} was null");
            }
            if (string.IsNullOrEmpty(ClientSecret))
            {
                throw new Exception($"{nameof(ClientSecret)} was null");
            }
        }
        
        public InitializeAccessTokenArg GetGoogleApiInitArg()
        {
            InitializeAccessTokenArg initializeAccessTokenArg = new InitializeAccessTokenArg
            {
                Code = Code,
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                RedirectUri = RedirectUri
            };
            return initializeAccessTokenArg;
        }
    }
}