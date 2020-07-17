using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AmoebaGameMatcherServer.Services.GoogleApi
{
   
    /// <summary>
    /// Отвечает за начисление товаров после совершения покупки в google play store
    /// </summary>
    public class PurchasesValidatorService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly PurchaseRegistrationService purchaseRegistrationService;
        private readonly GoogleApiPurchasesWrapperService googleApiPurchasesWrapperService;

        public PurchasesValidatorService(GoogleApiPurchasesWrapperService googleApiPurchasesWrapperService,
            PurchaseRegistrationService purchaseRegistrationService, ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.purchaseRegistrationService = purchaseRegistrationService;
            this.googleApiPurchasesWrapperService = googleApiPurchasesWrapperService;
        }

        [ItemCanBeNull]
        public async Task<bool> ValidateAsync([NotNull] string sku, [NotNull] string token)
        {
            try
            {
                string googleResponseJson = await googleApiPurchasesWrapperService.ValidateAsync(sku, token);
                bool responseIsOk = googleResponseJson != null;
                if (responseIsOk)
                {
                    Console.WriteLine($"{nameof(googleResponseJson)} {googleResponseJson}");
                    // string developerPayload = new GoogleResponseConverter().GetDeveloperPayload(googleResponseJson);
                    //
                    // Console.WriteLine($"{nameof(developerPayload)} "+developerPayload);
                    // Account account = await dbContext.Accounts
                    //     .Where(account1 => account1.ServiceId == developerPayload)
                    //     .SingleOrDefaultAsync();
                    //
                    // if (account == null)
                    // {
                    //     throw new Exception("Не удалось найти аккаунт который был указан в полезной нагрузке." +
                    //                         $"{nameof(developerPayload)} {developerPayload}");
                    // }
                    // else
                    // {
                    //     Console.WriteLine("аккаунт найден");
                    // }
                    //
                    // //внести данные про покупку в БД
                    // await purchaseRegistrationService.TryEnterPurchaseIntoDbAsync(googleResponseJson, sku, token, account.Id);

                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message+" "+e.StackTrace);
                return false;
            }
        }
    }

    // public class GoogleResponseConverter
    // {
    //     public string GetDeveloperPayload(string googleResponseJson)
    //     {
    //         dynamic googleResponseObj = JsonConvert.DeserializeObject(googleResponseJson);
    //         string developerPayloadWrapper = googleResponseObj["developerPayload"];
    //         dynamic jsonObj2 = JsonConvert.DeserializeObject(developerPayloadWrapper);
    //         string serviceId1 = jsonObj2["developerPayload"];
    //         string developerPayload = Encoding.UTF8.GetString(Convert.FromBase64String(serviceId1));
    //         return developerPayload;
    //     }
    // }
}