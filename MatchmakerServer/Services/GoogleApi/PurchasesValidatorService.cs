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
    /// Отвечает за начисление предметов после совершения покупки в google play store
    /// </summary>
    public class PurchasesValidatorService
    {
        private readonly GoogleApiPurchasesWrapperService googleApiPurchasesWrapperService;
        private readonly GoogleApiPurchaseAcknowledgeService googleApiPurchaseAcknowledgeService;
        private readonly PurchaseRegistrationService purchaseRegistrationService;
        private readonly ApplicationDbContext dbContext;

        public PurchasesValidatorService(GoogleApiPurchasesWrapperService googleApiPurchasesWrapperService, 
            GoogleApiPurchaseAcknowledgeService googleApiPurchaseAcknowledgeService,
             PurchaseRegistrationService purchaseRegistrationService, ApplicationDbContext dbContext)
        {
            this.googleApiPurchasesWrapperService = googleApiPurchasesWrapperService;
            this.googleApiPurchaseAcknowledgeService = googleApiPurchaseAcknowledgeService;
            this.purchaseRegistrationService = purchaseRegistrationService;
            this.dbContext = dbContext;
        }

        [ItemCanBeNull]
        public async Task<string[]> Validate([NotNull] string sku, [NotNull] string token)
        {
            string googleResponseJson = await googleApiPurchasesWrapperService.Validate(sku, token);
            bool responseIsOk = googleResponseJson != null;
            if (responseIsOk)
            {
                Console.WriteLine($"{nameof(googleResponseJson)} {googleResponseJson}");
                
                //TODO проверить что, полезная нагрузка содержит id игрока
                dynamic jsonObj = JsonConvert.DeserializeObject(googleResponseJson);
                string developerPayloadWrapper = jsonObj["developerPayload"];
                dynamic jsonObj2 = JsonConvert.DeserializeObject(developerPayloadWrapper);
                string serviceId = jsonObj2["developerPayload"];
                
                Console.WriteLine($"{nameof(serviceId)} "+serviceId);
                
                Account account = await dbContext.Accounts
                    .Where(account1 => account1.ServiceId == serviceId)
                    .SingleOrDefaultAsync();

                if (account == null)
                {
                    throw new Exception("Не удалось найти аккаунт который был указан в полезной нагрузке." +
                                        $"{nameof(serviceId)} {serviceId}");
                }

                //TODO внести данные про покупку в БД
                purchaseRegistrationService.EnterPurchaseIntoDb(googleResponseJson, sku, token, account.Id);

                //TODO  прочитать из БД и вернуть список названий подтверждённых продуктов
                var result = dbContext.Purchases
                    .Where(purchase => purchase.AccountId == account.Id && !purchase.IsPurchaseConfirmed)
                    .Select(purchase => purchase.Sku)
                    .ToArray();

                Console.WriteLine("result start");
                foreach (var s in result)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine("result end");
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}