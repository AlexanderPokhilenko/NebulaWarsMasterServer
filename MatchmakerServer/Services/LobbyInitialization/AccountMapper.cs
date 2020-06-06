using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Превращает Account, который зависит от EntityFramework, в сериализуемый AccountDto
    /// </summary>
    public class AccountMapper
    {
        public AccountDto Map(Account account)
        {
            AccountDto result = new AccountDto
            {
                Username = account.Username,
                AccountRating = account.Rating,
                SoftCurrency = account.SoftCurrency,
                HardCurrency = account.HardCurrency,
                BigLootboxPoints = 0,
                SmallLootboxPoints = account.SmallLootboxPoints,
                Warships = new List<WarshipDto>()
            };

            foreach (var warship in account.Warships)
            {
                WarshipDto warshipDto = new WarshipDto();
                warshipDto.Rating = warship.WarshipRating;
                warshipDto.CombatRoleName = warship.WarshipType.WarshipCombatRole.Name;
                warshipDto.Description = warship.WarshipType.Description;
                warshipDto.PrefabName = warship.WarshipType.Name;
                warshipDto.PowerPoints = warship.PowerPoints;
                warshipDto.Id = warship.Id;

                result.Warships.Add(warshipDto);
            }

            return result;
        }
    }
}