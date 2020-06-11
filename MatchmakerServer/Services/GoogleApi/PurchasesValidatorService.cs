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
            string responseContentJson = await googleApiPurchasesWrapperService.Validate(sku, token);
            bool responseIsOk = responseContentJson != null; 
            if (responseIsOk)
            {
                Console.WriteLine($"{nameof(responseContentJson)} {responseContentJson}");
                //TODO внести данные про покупку в БД
                purchaseRegistrationService.EnterPurchaseIntoDb(responseContentJson);
                dynamic jsonDich1 = JsonConvert.DeserializeObject(responseContentJson);
                string developerPayload1 = jsonDich1["developerPayload"];
                dynamic jsonDich2 = JsonConvert.DeserializeObject(developerPayload1);
                string developerPayload2 = jsonDich2["developerPayload"];
                
                Console.WriteLine();
                Console.WriteLine($"{nameof(developerPayload1)} {developerPayload1}");
                Console.WriteLine($"{nameof(developerPayload2)} {developerPayload2}");
                Console.WriteLine();
                //уведомить google о регистрации покупки
                try
                {
                    await googleApiPurchaseAcknowledgeService.Acknowledge(sku, token, developerPayload1);
                    Console.WriteLine("Удалось\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message+" "+e.StackTrace);
                }
                
                try
                {
                    await googleApiPurchaseAcknowledgeService.Acknowledge(sku, token, developerPayload2);
                    Console.WriteLine("Удалось\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message+" "+e.StackTrace);
                }
                
                try
                {
                    JObject jObj = new JObject();
                    jObj.Add("developerPayload", developerPayload2);
                    string developerPayload3 = jObj.ToString();
                    await googleApiPurchaseAcknowledgeService.Acknowledge(sku, token, developerPayload3);
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