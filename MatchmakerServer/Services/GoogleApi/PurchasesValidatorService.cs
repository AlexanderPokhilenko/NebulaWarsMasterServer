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
        public async Task<string[]> ValidateAsync([NotNull] string sku, [NotNull] string token)
        {
            string googleResponseJson = await googleApiPurchasesWrapperService.ValidateAsync(sku, token);
            bool responseIsOk = googleResponseJson != null;
            if (responseIsOk)
            {
                Console.WriteLine($"{nameof(googleResponseJson)} {googleResponseJson}");
                string developerPayload = new GoogleResponseConverter().GetDeveloperPayload(googleResponseJson);
                
                Console.WriteLine($"{nameof(developerPayload)} "+developerPayload);
                Account account = await dbContext.Accounts
                    .Where(account1 => account1.ServiceId == developerPayload)
                    .SingleOrDefaultAsync();
            
                if (account == null)
                {
                    throw new Exception("Не удалось найти аккаунт который был указан в полезной нагрузке." +
                                        $"{nameof(developerPayload)} {developerPayload}");
                }
                else
                {
                    Console.WriteLine("аккаунт найден");
                }
            
                //внести данные про покупку в БД
                await purchaseRegistrationService.TryEnterPurchaseIntoDbAsync(googleResponseJson, sku, token, account.Id);
            
                //прочитать из БД и вернуть список названий подтверждённых продуктов
                var result = dbContext.Purchases
                    .Where(purchase => purchase.AccountId == account.Id && !purchase.IsConfirmed)
                    .Select(purchase => purchase.Sku)
                    .ToArray();
                
                Console.WriteLine("result start");
                foreach (var s in result)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine("result end");
                return null;
            }
            else
            {
                return null;
            }
        }
    }

    public class GoogleResponseConverter
    {
        public string GetDeveloperPayload(string googleResponseJson)
        {
            dynamic googleResponseObj = JsonConvert.DeserializeObject(googleResponseJson);
            string developerPayloadWrapper = googleResponseObj["developerPayload"];
            dynamic jsonObj2 = JsonConvert.DeserializeObject(developerPayloadWrapper);
            string serviceId1 = jsonObj2["developerPayload"];
            string developerPayload = Encoding.UTF8.GetString(Convert.FromBase64String(serviceId1));
            return developerPayload;
        }
    }
}