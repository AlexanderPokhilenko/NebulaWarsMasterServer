using System.Threading.Tasks;

namespace AmoebaGameMatcherServer.Services.MatchFinishing
{
    public interface IWarshipRatingReaderService
    {
        Task<int> ReadWarshipRatingAsync(int warshipId);
    }
}