using System;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;

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
                //уведомить google о регистрации покупки
                await googleApiPurchaseAcknowledgeService.Acknowledge(sku, token);
            }
        }
    }
}