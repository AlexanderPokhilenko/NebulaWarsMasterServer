using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    //TODO сука блять эта поебота не маппит вложенный запрос
    /// <summary>
    /// Достаёт из БД данные про корабли аккаунта.
    /// </summary>
    public class DbAccountWarshipsReader
    {
        private readonly ApplicationDbContext dbContext;

        public DbAccountWarshipsReader(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [ItemCanBeNull]
        public async Task<AccountDbDto> GetAccountWithWarshipsAsync([NotNull] string serviceId)
        {
            //todo это пизда
            Account account = await dbContext.Accounts
                .Where(account1 => account1.ServiceId == serviceId)
                .SingleOrDefaultAsync();

            if (account == null)
            {
                return null;
            }

            //todo пиздец
            int accountRating = await dbContext.Increments
                                    .Include(increment => increment.Resource)
                                    .ThenInclude(resource => resource.Transaction)
                                    .ThenInclude(transaction => transaction.Account)
                                    .Where(increment => increment.IncrementTypeId == IncrementTypeEnum.WarshipRating
                                                        && increment.Resource.Transaction.Account.ServiceId ==
                                                        serviceId)
                                    .Select(increment => increment.Amount)
                                    .DefaultIfEmpty()
                                    .SumAsync()
                                -
                                await dbContext.Decrements
                                    .Include(increment => increment.Resource)
                                    .ThenInclude(resource => resource.Transaction)
                                    .ThenInclude(transaction => transaction.Account)
                                    .Where(decrement => decrement.DecrementTypeId == DecrementTypeEnum.WarshipRating
                                                        && decrement.Resource.Transaction.Account.ServiceId ==
                                                        serviceId)
                                    .Select(increment => increment.Amount)
                                    .DefaultIfEmpty()
                                    .SumAsync()
                ;
            
            
            AccountDbDto accountDbDto = new AccountDbDto
            {
                Username = account.Username,
                ServiceId = account.ServiceId,
                RegistrationDateTime = account.RegistrationDateTime,
                Warships = new List<WarshipDbDto>(),
                Rating = accountRating,
                Id = account.Id
            };

            //todo кусок говна
            List<Warship> warships = await dbContext.Warships
                .Include(warship => warship.Account)
                .Include(warship => warship.WarshipType)
                    .ThenInclude(warshipType => warshipType.WarshipCombatRole)
                .Where(warship => warship.Account.ServiceId == serviceId)
                .ToListAsync();

            //todo куча запросов вместо одного
            foreach (Warship warship in warships)
            {
                Console.WriteLine($"warship.Id "+warship.Id);
                WarshipDbDto warshipDbDto = new WarshipDbDto
                {
                    WarshipType = warship.WarshipType,
                    Id = warship.Id
                };

                warshipDbDto.WarshipPowerLevel = await dbContext.Increments
                    .Where(increment => increment.WarshipId == warship.Id
                                        && increment.IncrementTypeId == IncrementTypeEnum.WarshipLevel)
                    .MaxAsync(increment => increment.Amount);

                if (warshipDbDto.WarshipPowerLevel == 0)
                {
                    throw new Exception("Сука блять какого хуя уровень нулевой?");
                }

                Console.WriteLine("warshipDbDto.WarshipPowerLevel "+warshipDbDto.WarshipPowerLevel);
                
                warshipDbDto.WarshipPowerPoints = await dbContext.Increments
                       .Where(increment => increment.WarshipId == warship.Id
                                           && increment.IncrementTypeId == IncrementTypeEnum.WarshipPowerPoints)
                       .DefaultIfEmpty()
                       .SumAsync(increment => increment.Amount)
                   -
                   await dbContext.Decrements
                       .Where(decrement => decrement.WarshipId == warship.Id
                                           && decrement.DecrementTypeId == DecrementTypeEnum.WarshipPowerPoints)
                       .DefaultIfEmpty()
                       .SumAsync(decrement => decrement.Amount);

                Console.WriteLine($"warshipDbDto.WarshipPowerPoints = "+warshipDbDto.WarshipPowerPoints);
                
                warshipDbDto.WarshipRating = await dbContext.Increments
                         .Where(increment => increment.WarshipId == warship.Id
                                             && increment.IncrementTypeId == IncrementTypeEnum.WarshipRating)
                         .DefaultIfEmpty()
                         .SumAsync(increment => increment.Amount)
                     -
                     await dbContext.Decrements
                         .Where(decrement => decrement.WarshipId == warship.Id 
                                             && decrement.DecrementTypeId == DecrementTypeEnum.WarshipRating)
                         .DefaultIfEmpty()
                         .SumAsync(decrement => decrement.Amount);
                
                accountDbDto.Warships.Add(warshipDbDto);
            }

            return accountDbDto;
        }
    }
}