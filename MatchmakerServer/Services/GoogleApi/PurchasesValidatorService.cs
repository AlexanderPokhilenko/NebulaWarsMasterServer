using System;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public PurchasesValidatorService(GoogleApiPurchasesWrapperService googleApiPurchasesWrapperService, 
            GoogleApiPurchaseAcknowledgeService googleApiPurchaseAcknowledgeService,
             PurchaseRegistrationService purchaseRegistrationService)
        {
            this.googleApiPurchasesWrapperService = googleApiPurchasesWrapperService;
            this.googleApiPurchaseAcknowledgeService = googleApiPurchaseAcknowledgeService;
            this.purchaseRegistrationService = purchaseRegistrationService;
        }

        public async Task Validate([NotNull] string sku, [NotNull] string token)
        {
            string googleResponseJson = await googleApiPurchasesWrapperService.Validate(sku, token);
            bool responseIsOk = googleResponseJson != null; 
            if (responseIsOk)
            {
                Console.WriteLine($"{nameof(googleResponseJson)} {googleResponseJson}");
                //TODO внести данные про покупку в БД
                purchaseRegistrationService.EnterPurchaseIntoDb(googleResponseJson);
                
                //уведомить google о регистрации покупки
                dynamic jsonDich1 = JsonConvert.DeserializeObject(googleResponseJson);
                string developerPayload1 = jsonDich1["developerPayload"];
                Console.WriteLine($"{nameof(developerPayload1)} {developerPayload1}");
                dynamic jsonDich2 = JsonConvert.DeserializeObject(developerPayload1);
                string developerPayload2 = jsonDich2["developerPayload"];
                Console.WriteLine($"{nameof(developerPayload2)} {developerPayload2}");
             
                try
                {
                    await googleApiPurchaseAcknowledgeService.Acknowledge(sku, token, developerPayload2);
                    Console.WriteLine("Удалось\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message+" "+e.StackTrace);
                }
            }
        }
    }
}