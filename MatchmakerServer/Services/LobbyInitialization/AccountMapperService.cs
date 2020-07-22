using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    /// <summary>
    /// Превращает AccountDbDto, в сериализуемый AccountDto
    /// </summary>
    public class AccountMapperService
    {
        private readonly WarshipsCharacteristicsService warshipsCharacteristicsService;
        public AccountMapperService(WarshipsCharacteristicsService warshipsCharacteristicsService)
        {
            this.warshipsCharacteristicsService = warshipsCharacteristicsService;
        }
        
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
                Warships = new List<WarshipDto>(),
                AccountId = account.Id
            };

            foreach (WarshipDbDto warshipDbDto in account.Warships)
            {
                WarshipDto warshipDto = new WarshipDto();
                warshipDto.Rating = warshipDbDto.WarshipRating;
                warshipDto.CombatRoleName = warshipDbDto.WarshipType.WarshipCombatRole.Name;
                warshipDto.Description = warshipDbDto.WarshipType.Description;
                warshipDto.WarshipName = warshipDbDto.WarshipType.Name;
                warshipDto.PowerPoints = warshipDbDto.WarshipPowerPoints;
                warshipDto.Id = warshipDbDto.Id;
                warshipDto.PowerLevel = warshipDbDto.WarshipPowerLevel;
                warshipDto.WarshipCharacteristics = warshipsCharacteristicsService
                    .GetWarshipCharacteristics(warshipDbDto.WarshipType.Id);
               

                List<SkinTypeDto> skinTypeDtos = new List<SkinTypeDto>();
                foreach (SkinType skinType in warshipDbDto.Skins)
                {
                    SkinTypeDto skinTypeDto = new SkinTypeDto()
                    {
                        Id = skinType.Id,
                        Name = skinType.Name
                    };
                    skinTypeDtos.Add(skinTypeDto);
                }
                warshipDto.Skins = skinTypeDtos;

                warshipDto.CurrentSkinIndex = warshipDto.Skins
                    .FindIndex((skin) => skin.Id == warshipDbDto.CurrentSkinTypeId);
                
               
                result.Warships.Add(warshipDto);
            }

            return result;
        }

    
    }
}