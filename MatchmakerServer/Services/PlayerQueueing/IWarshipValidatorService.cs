using System.Threading.Tasks;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services.PlayerQueueing
{
    public interface IWarshipValidatorService
    {
        Task<(bool success, Warship warship)> GetWarshipById(string playerServiceId, int warshipId);
    }
}