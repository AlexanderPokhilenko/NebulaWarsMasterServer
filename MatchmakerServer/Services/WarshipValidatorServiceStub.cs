using System;
using System.Threading.Tasks;
using DataLayer.Tables;

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
                    ServiceId = "someId"
                }
            };
            return new ValueTuple<bool, Warship>(true, warship);
        }
    }
}