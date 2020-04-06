using System;

namespace AmoebaGameMatcherServer.Services.MatchCreationInitiation
{
    public class PlayerTimeoutManagerServiceStub:IPlayerTimeoutManager
    {
        public bool IsWaitingTimeExceeded()
        {
            Console.WriteLine(nameof(IsWaitingTimeExceeded));
            return true;
        }
    }
}