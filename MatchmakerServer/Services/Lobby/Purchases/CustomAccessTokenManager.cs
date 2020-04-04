using System;
using System.IO;
using System.Threading.Tasks;
using DataLayer;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Отвечает за обновление accessToken-а и сохранение данных в файл на случай перезагрузки.
    /// </summary>
    public class CustomGoogleApiAccessTokenService
    {
        private MyGoogleApiData apiData;
        private readonly object lockObj = new object();
        
        public async Task Initialize()
        {
            if (apiData != null)
            {
                throw new Exception($"{nameof(apiData)} not null. Произошёл повторный вызов инициализации?");
            }
            
            try
            {
                GoogleApiGlobals.Check();
                
                string currentDirectory = GoogleApiFileManager.GetCurrentDirectory();
                Console.WriteLine($"{nameof(currentDirectory)} {currentDirectory}");
                
                if(GoogleApiGlobals.RecreateGoogleApiFile)
                {
                    GoogleApiFileManager.RemoveFile();
                }
                
                MyGoogleApiData apiDataFromFile = await GoogleApiFileManager.GetApiDataFromFile();
                if (MyGoogleApiData.IsCorrect(apiDataFromFile, out string error1))
                {
                    Console.WriteLine("Установлены данные из файла");
                    apiData = apiDataFromFile;
                }
                else
                {
                    Console.WriteLine(error1);
                    Console.WriteLine("Создание нового refresh токена");
                    var initializeAccessTokenArg = GoogleApiGlobals.GetGoogleApiInitArg();
                    apiData = await TokenManagerService.InitializeAccessTokenAsync(initializeAccessTokenArg);
                    await GoogleApiFileManager.WriteGoogleApiDataToFile(apiData);
                }

                if (MyGoogleApiData.IsCorrect(apiData, out string error2))
                {
                    if (apiData.ExpiresInSec != null)
                    {
#pragma warning disable 4014
                        StartEndlessAccessTokenUpdatingAsync(2).ConfigureAwait(true);
                        // StartEndlessAccessTokenUpdatingAsync(apiData.ExpiresInSec.Value).ConfigureAwait(true);
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
                if (apiData != null)
                {
                    AccessTokenUpdatingArg tokenUpdatingArg = new AccessTokenUpdatingArg
                    {
                        ClientId = GoogleApiGlobals.ClientId,
                        ClientSecret = GoogleApiGlobals.ClientSecret,
                        RefreshToken = apiData.RefreshToken
                    };
                    var result = TokenManagerService.UpdateAccessToken(tokenUpdatingArg).Result;
                    lock (lockObj)
                    {
                        apiData.AccessToken = result.AccessToken;
                    }
                    delaySec = result.ExpiresInSec;
                }
                else
                {
                    throw new Exception($"{nameof(apiData)} was null");
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        public string GetAccessToken()
        {
            lock (lockObj)
            {
                if (apiData != null)
                {
                    if (apiData.AccessToken != null)
                    {
                        return string.Copy(apiData.AccessToken);
                    }
                    else
                    {
                        throw new Exception($"{nameof(apiData.AccessToken)} was null");
                    }
                }
                else
                {
                    throw new Exception($"{nameof(apiData)} was null");
                }
            }
        }
    }
}
