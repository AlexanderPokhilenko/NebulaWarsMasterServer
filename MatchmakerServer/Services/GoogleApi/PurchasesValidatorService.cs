using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.Http.Utils;
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
        private readonly RealPurchaseTransactionFactoryService realPurchaseTransactionFactory;

        public PurchasesValidatorService(GoogleApiPurchasesWrapperService googleApiPurchasesWrapperService,
            PurchaseRegistrationService purchaseRegistrationService, ApplicationDbContext dbContext, 
            RealPurchaseTransactionFactoryService realPurchaseTransactionFactory)
        {
            this.dbContext = dbContext;
            this.realPurchaseTransactionFactory = realPurchaseTransactionFactory;
            this.purchaseRegistrationService = purchaseRegistrationService;
            this.googleApiPurchasesWrapperService = googleApiPurchasesWrapperService;
        }

        public async Task<bool> ValidateAsync([NotNull] string sku, [NotNull] string token)
        {
            try
            {
                string googleResponseJson = await googleApiPurchasesWrapperService.ValidateAsync(sku, token);
                bool responseIsOk = googleResponseJson != null;
                if (responseIsOk)
                {
                    Console.WriteLine($"{nameof(googleResponseJson)} {googleResponseJson}");
                    GoogleResponse googleResponse = JsonConvert.DeserializeObject<GoogleResponse>(googleResponseJson);

                    string accountServiceId = googleResponse.ObfuscatedExternalAccountId.Caesar(-10);
                    Account account = await dbContext.Accounts
                        .Where(account1 => account1.ServiceId == accountServiceId)
                        .SingleOrDefaultAsync();
                    if (account == null)
                    {
                        throw new Exception("Не удалось найти аккаунт который был указан в полезной нагрузке." +
                                            $"{nameof(accountServiceId)} {accountServiceId}");
                    }

                    Console.WriteLine("аккаунт найден");

                    // внести данные про покупку в БД
                    int realPurchaseModelId = await purchaseRegistrationService
                        .WriteAndGetId(account.Id, sku, googleResponseJson);

                    //записать транзакцию
                    Transaction transaction = realPurchaseTransactionFactory.Create(account.Id, sku, realPurchaseModelId);
                    
                    //todo проверить транзакцию на адекватность
                    await dbContext.Transactions.AddAsync(transaction);
                    await dbContext.SaveChangesAsync();
                    
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
}