using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.PlayerQueueing;
using DataLayer.Tables;
using MatchmakerTest;

namespace AmoebaGameMatcherServer.Services
{   
    
    public class WarshipValidatorServiceStub:IWarshipValidatorService
    {
#pragma warning disable 1998
        public async Task<(bool success, Warship warship)> GetWarshipById(string playerServiceId, int warshipId)
#pragma warning restore 1998
        {
            Warship warship = new Warship
            {
                WarshipType = new WarshipType(),
                Account = new Account
                {
                    ServiceId = UniqueStringFactory.Create()
                }
            };
            return new ValueTuple<bool, Warship>(true, warship);
        }
    }
}