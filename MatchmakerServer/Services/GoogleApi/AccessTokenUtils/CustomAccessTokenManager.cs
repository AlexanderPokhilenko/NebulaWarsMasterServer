using System;
using System.Threading.Tasks;
using DataLayer;
using Newtonsoft.Json;

namespace AmoebaGameMatcherServer.Services.GoogleApi.AccessTokenUtils
{
    /// <summary>
    /// Отвечает за обновление accessToken-а и сохранение данных в файл на случай перезагрузки.
    /// </summary>
    public class CustomGoogleApiAccessTokenService
    {
        private string accessToken;
        private readonly object lockObj = new object();
        private readonly GoogleApiProfileStorageService profileStorageService;
        
        public CustomGoogleApiAccessTokenService(GoogleApiProfileStorageService profileStorageService)
        {
            this.profileStorageService = profileStorageService;
        }
        
        public async Task Initialize()
        {
            GoogleApiProfile googleApiProfile = profileStorageService.GetCurrentProfile();
            try
            {
                GoogleApiAuthData authData;
                bool haveRefreshToken = googleApiProfile.GoogleApiData != null; 
                if (haveRefreshToken)
                {
                    Console.WriteLine("Refresh token уже есть");
                    authData = JsonConvert
                        .DeserializeObject<GoogleApiAuthData>(googleApiProfile.GoogleApiData);
                }
                else
                {
                    Console.WriteLine("Создание нового refresh токена");
                    InitializeAccessTokenArg initializeAccessTokenArg = googleApiProfile.GetGoogleApiInitArg();
                    authData = await TokenManagerService.CreateRefreshTokenAsync(initializeAccessTokenArg);
                    string text = JsonConvert.SerializeObject(authData);
                    Console.WriteLine(text);
                    throw new Exception("Введите refresh token в текущий google api profile "+text);
                }
            
                if (authData != null)
                {
                    AccessTokenUpdatingArg test = new AccessTokenUpdatingArg()
                    {
                        ClientId = googleApiProfile.ClientId,
                        ClientSecret = googleApiProfile.ClientSecret,
                        RefreshToken = authData.RefreshToken
                    };
                    Task task = StartEndlessAccessTokenUpdatingAsync(test);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message+" "+e.StackTrace);
            }
        }

        private async Task StartEndlessAccessTokenUpdatingAsync(AccessTokenUpdatingArg tokenUpdatingArg, int firstDelaySec=2)
        {
            int delaySec = firstDelaySec;
            while (true)
            {
                await Task.Delay(1000 * delaySec);
                RefreshedData result = TokenManagerService.UpdateAccessToken(tokenUpdatingArg).Result;
                lock (lockObj)
                {
                    accessToken = result.AccessToken;
                }
                delaySec = result.ExpiresInSec;
              
            }
            // ReSharper disable once FunctionNeverReturns
        }

        public string GetAccessToken()
        {
            lock (lockObj)
            {
                if (accessToken != null)
                {
                    return string.Copy(accessToken);
                }

                throw new Exception($"{nameof(accessToken)} was null");
            }
        }
    }
}
