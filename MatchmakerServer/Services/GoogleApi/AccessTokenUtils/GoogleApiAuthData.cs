using System;

namespace AmoebaGameMatcherServer.Services.GoogleApi
{
    public static class Extension
    {
        public static bool Check(this GoogleApiAuthData apiAuthDataArg, out string error)
        {
            if (apiAuthDataArg != null)
            {
                if (apiAuthDataArg.AccessToken == null)
                {
                    error = $"{nameof(apiAuthDataArg.AccessToken)} was null";
                    return false;
                }
                if (apiAuthDataArg.RefreshToken == null)
                {
                    error = $"{nameof(apiAuthDataArg.RefreshToken)} was null";
                    return false;
                }
                if (apiAuthDataArg.ExpiresInSec == null)
                {
                    error = $"{nameof(apiAuthDataArg.ExpiresInSec)} was null";
                    return false;
                }
                if (apiAuthDataArg.AccessTokenCreationTime == null)
                {
                    error = $"{nameof(apiAuthDataArg.AccessTokenCreationTime)} was null";
                    return false;
                }
                error = null;
                return true;
            }
            else
            {
                error = $"{nameof(apiAuthDataArg)} was null";
                return false;
            }
        }
    }
    
    public class GoogleApiAuthData
    {
        public string AccessToken;
        public string RefreshToken;
        public int? ExpiresInSec;
        public DateTime? AccessTokenCreationTime;
    }
}