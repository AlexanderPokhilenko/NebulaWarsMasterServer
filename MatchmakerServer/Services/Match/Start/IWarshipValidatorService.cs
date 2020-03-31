using System.Threading.Tasks;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services
{
    public interface IWarshipValidatorService
    {
        Task<(bool success, Warship warship)> GetWarshipById(string playerServiceId, int warshipId);
    }
}