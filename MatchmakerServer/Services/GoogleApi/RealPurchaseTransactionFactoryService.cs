using System;
using System.Linq;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services.GoogleApi
{
    public class RealPurchaseTransactionFactoryService
    {
        private readonly HardCurrencyTransactionFactory hardCurrencyTransactionFactory;

        public RealPurchaseTransactionFactoryService(HardCurrencyTransactionFactory hardCurrencyTransactionFactory)
        {
            this.hardCurrencyTransactionFactory = hardCurrencyTransactionFactory;
        }

        public Transaction Create(int accountId, string sku, int realPurchaseModelId)
        {
            if (sku.Contains("hard_currency"))
            {
                string amountStr = sku.Split('_').Last();
                int amount = int.Parse(amountStr);
                return hardCurrencyTransactionFactory.Create(accountId, amount, realPurchaseModelId);
            }
            else
            {
                throw new Exception("Неизвестный тип продукта за реальную валюту");
            }
        }
    }
}