using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Tables;
using JetBrains.Annotations;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    /// <summary>
    /// Читает всю информацию про корабли аккаунта.
    /// </summary>
    public class DbAccountWarshipReaderService
    {
        private readonly SkinsDbReaderService skinsDbReaderService;
        private readonly DbWarshipsStatisticsReader dbWarshipsStatisticsReader;
        
        public DbAccountWarshipReaderService(DbWarshipsStatisticsReader dbWarshipsStatisticsReader,
            SkinsDbReaderService skinsDbReaderService)
        {
            this.dbWarshipsStatisticsReader = dbWarshipsStatisticsReader;
            this.skinsDbReaderService = skinsDbReaderService;
        }
        
        [ItemCanBeNull]
        public async Task<AccountDbDto> ReadAsync(string playerServiceId)
        {
            AccountDbDto accountDbDto = await dbWarshipsStatisticsReader.ReadAsync(playerServiceId);
            if (accountDbDto == null)
            {
                return null;
            }

            //заполнить скины для всех кораблей
            Dictionary<int, List<string>> skinsDict = await skinsDbReaderService.ReadAsync(accountDbDto.Id);
            if (skinsDict == null || skinsDict.Count == 0)
            {
                throw new Exception("warship has no skin");
            }
            
            foreach (var (warshipId, list) in skinsDict)
            {
                accountDbDto.Warships
                    .Single(warship => warship.Id == warshipId)
                    .Skins.AddRange(list);
            }

            foreach (var warshipDbDto in accountDbDto.Warships)
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