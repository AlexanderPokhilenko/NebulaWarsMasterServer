using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    public class DbAccountWarshipReaderService : IDbAccountWarshipReaderService
    {
        private readonly ISkinsDbReaderService skinsDbReaderService;
        private readonly IDbWarshipsStatisticsReader dbWarshipsStatisticsReader;
        
        public DbAccountWarshipReaderService(IDbWarshipsStatisticsReader dbWarshipsStatisticsReader,
            ISkinsDbReaderService skinsDbReaderService)
        {
            this.skinsDbReaderService = skinsDbReaderService;
            this.dbWarshipsStatisticsReader = dbWarshipsStatisticsReader;
        }
        
        public async Task<AccountDbDto> ReadAsync(string playerServiceId)
        {
            AccountDbDto accountDbDto = await dbWarshipsStatisticsReader.ReadAsync(playerServiceId);
            if (accountDbDto == null)
            {
                return null;
            }

            // foreach (var warshipDbDto in accountDbDto.Warships)
            // {
            //     Console.WriteLine("очки силы корабля "+warshipDbDto.WarshipPowerPoints);
            // }

            //заполнить список скинов для всех типов кораблей
            //warshipId, список скинов
            Dictionary<int, List<SkinType>> skinsDict = await skinsDbReaderService.ReadAsync(accountDbDto.Id);
            if (skinsDict == null || skinsDict.Count == 0)
            {
                throw new Exception("warship has no skin");
            }
            
            foreach ((int warshipId, List<SkinType> list) in skinsDict)
            {
                WarshipDbDto warship = accountDbDto.Warships.Single(warship1 => warship1.Id == warshipId);
                warship.Skins.AddRange(list);
                warship.CurrentSkinType = list
                    .Single(skinType => skinType.Id == warship.CurrentSkinTypeId);
            }

            foreach (WarshipDbDto warshipDbDto in accountDbDto.Warships)
            {
                if (warshipDbDto.WarshipPowerLevel == 0)
                {
                    throw new Exception("Нулевой уровень "+nameof(AccountDbReaderService));
                }
                
                if (warshipDbDto.Skins == null || warshipDbDto.Skins.Count == 0)
                {
                    throw new Exception("Warship have no skins");
                }
            }

            return accountDbDto;
        }
    }
}