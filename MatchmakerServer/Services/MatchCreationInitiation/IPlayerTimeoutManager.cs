namespace AmoebaGameMatcherServer.Services.MatchCreationInitiation
{
    public interface IPlayerTimeoutManager
    {
        bool IsWaitingTimeExceeded();
    }
}