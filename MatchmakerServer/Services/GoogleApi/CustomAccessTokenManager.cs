using System;
using System.Threading.Tasks;
using DataLayer;

namespace AmoebaGameMatcherServer.Services.GoogleApi
{
    /// <summary>
    /// Отвечает за обновление accessToken-а и сохранение данных в файл на случай перезагрузки.
    /// </summary>
    public class CustomGoogleApiAccessTokenService
    {
        private GoogleApiAuthData apiAuthData;
        private readonly object lockObj = new object();
        
        public async Task Initialize()
        {
            if (apiAuthData != null)
            {
                throw new Exception($"{nameof(apiAuthData)} not null. Инициализация уже была проведена.");
            }
            
            try
            {
                GoogleApiGlobals.CheckNull();
                
                string currentDirectory = GoogleApiFileManager.GetCurrentDirectory();
                Console.WriteLine($"{nameof(currentDirectory)} {currentDirectory}");
                
                if(GoogleApiGlobals.RecreateGoogleApiFile)
                {
                    GoogleApiFileManager.RemoveFile();
                }
                
                GoogleApiAuthData apiAuthDataFromFile = await GoogleApiFileManager.ReadApiDataFromFile();
                if (apiAuthDataFromFile.Check(out string error1))
                {
                    Console.WriteLine("Установлены данные из файла");
                    apiAuthData = apiAuthDataFromFile;
                }
                else
                {
                    Console.WriteLine(error1);
                    Console.WriteLine("Создание нового refresh токена");
                    var initializeAccessTokenArg = GoogleApiGlobals.GetGoogleApiInitArg();
                    apiAuthData = await TokenManagerService.InitializeAccessTokenAsync(initializeAccessTokenArg);
                    await GoogleApiFileManager.WriteGoogleApiDataToFile(apiAuthData);
                }

                if (apiAuthData.Check(out string error2))
                {
                    if (apiAuthData.ExpiresInSec != null)
                    {
#pragma warning disable 4014
                        StartEndlessAccessTokenUpdatingAsync(2).ConfigureAwait(true);
                        // StartEndlessAccessTokenUpdatingAsync(apiAuthData.ExpiresInSec.Value).ConfigureAwait(true);
#pragma warning restore 4014
                    }
                }
                else
                {
                    throw new Exception(error2);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task StartEndlessAccessTokenUpdatingAsync(int firstDelaySec)
        {
            int delaySec = firstDelaySec;
            while (true)
            {
                await Task.Delay(1000 * delaySec);
                if (apiAuthData != null)
                {
                    AccessTokenUpdatingArg tokenUpdatingArg = new AccessTokenUpdatingArg
                    {
                        ClientId = GoogleApiGlobals.ClientId,
                        ClientSecret = GoogleApiGlobals.ClientSecret,
                        RefreshToken = apiAuthData.RefreshToken
                    };
                    var result = TokenManagerService.UpdateAccessToken(tokenUpdatingArg).Result;
                    lock (lockObj)
                    {
                        apiAuthData.AccessToken = result.AccessToken;
                    }
                    delaySec = result.ExpiresInSec;
                }
                else
                {
                    throw new Exception($"{nameof(apiAuthData)} was null");
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        public string GetAccessToken()
        {
            lock (lockObj)
            {
                if (apiAuthData != null)
                {
                    if (apiAuthData.AccessToken != null)
                    {
                        return string.Copy(apiAuthData.AccessToken);
                    }
                    else
                    {
                        throw new Exception($"{nameof(apiAuthData.AccessToken)} was null");
                    }
                }
                else
                {
                    throw new Exception($"{nameof(apiAuthData)} was null");
                }
            }
        }
    }
}
