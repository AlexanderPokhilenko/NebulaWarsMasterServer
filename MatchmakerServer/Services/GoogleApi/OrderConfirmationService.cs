using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

public class OrderConfirmationService
{
    private readonly ApplicationDbContext dbContext;

    public OrderConfirmationService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> TryConfirmOrder([NotNull] string serviceId, [NotNull] string sku)
    {
        try
        {
            var purchase = await dbContext.Purchases
                .Include(purchase1 => purchase1.Account)
                .Where(purchase1 =>
                    purchase1.Account.ServiceId == serviceId && purchase1.Sku == sku && !purchase1.IsConfirmed)
                .SingleOrDefaultAsync();

            if (purchase != null)
            {
                purchase.IsConfirmed = true;
                await dbContext.SaveChangesAsync();
                Console.WriteLine($"Успешная пометка продукта {purchase.Sku}");
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message+" "+e.StackTrace);
        }

        return false;
}
}