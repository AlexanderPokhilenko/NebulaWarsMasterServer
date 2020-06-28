using System;
using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    //todo написать тесты на эту поеботу
    /// <summary>
    /// Превращает AccountDbDto, в сериализуемый AccountDto
    /// </summary>
    public class AccountMapper
    {
        public AccountDto Map(AccountDbDto account)
        {
            AccountDto result = new AccountDto
            {
                Username = account.Username,
                AccountRating = account.Rating,
                SoftCurrency = account.SoftCurrency,
                HardCurrency = account.HardCurrency,
                BigLootboxPoints = 0,
                SmallLootboxPoints = account.LootboxPoints,
                Warships = new List<WarshipDto>()
            };

            foreach (var warship in account.Warships)
            {
                WarshipDto warshipDto = new WarshipDto();
                warshipDto.Rating = warship.WarshipRating;
                warshipDto.CombatRoleName = warship.WarshipType.WarshipCombatRole.Name;
                warshipDto.Description = warship.WarshipType.Description;
                warshipDto.WarshipName = warship.WarshipType.Name;
                warshipDto.PowerPoints = warship.WarshipPowerPoints;
                warshipDto.Id = warship.Id;
                warshipDto.ViewTypeId = GetViewTypeByName(warshipDto.WarshipName);
                warshipDto.PowerLevel = warship.WarshipPowerLevel;
                
                result.Warships.Add(warshipDto);
            }

            return result;
        }

        private ViewTypeId GetViewTypeByName(string warshipDtoWarshipName)
        {
            switch (warshipDtoWarshipName)
            {
                case "hare":
                    return ViewTypeId.HareShip;
                case "bird":
                    return ViewTypeId.BirdPlayer;
                case "smiley":
                    return ViewTypeId.SmileyPlayer;
                default:
                    throw new ArgumentOutOfRangeException(nameof(warshipDtoWarshipName));
            }
        }
    }
}