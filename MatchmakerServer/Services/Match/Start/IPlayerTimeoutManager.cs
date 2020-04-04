namespace AmoebaGameMatcherServer.Services
{
    public interface IPlayerTimeoutManager
    {
        bool IsWaitingTimeExceeded();
    }
}