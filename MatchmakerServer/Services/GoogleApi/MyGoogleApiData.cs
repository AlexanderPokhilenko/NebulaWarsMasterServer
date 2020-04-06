using System;

namespace AmoebaGameMatcherServer.Services.GoogleApi
{
    public class MyGoogleApiData
    {
        public string AccessToken;
        public string RefreshToken;
        public int? ExpiresInSec;
        public DateTime? AccessTokenCreationTime;
        
        public static bool IsCorrect(MyGoogleApiData apiDataArg, out string error)
        {
            if (apiDataArg != null)
            {
                if (apiDataArg.AccessToken == null)
                {
                    error = $"{nameof(apiDataArg.AccessToken)} was null";
                    return false;
                }
                if (apiDataArg.RefreshToken == null)
                {
                    error = $"{nameof(apiDataArg.RefreshToken)} was null";
                    return false;
                }
                if (apiDataArg.ExpiresInSec == null)
                {
                    error = $"{nameof(apiDataArg.ExpiresInSec)} was null";
                    return false;
                }
                if (apiDataArg.AccessTokenCreationTime == null)
                {
                    error = $"{nameof(apiDataArg.AccessTokenCreationTime)} was null";
                    return false;
                }
                error = null;
                return true;
            }
            else
            {
                error = $"{nameof(apiDataArg)} was null";
                return false;
            }
        }
    }
}