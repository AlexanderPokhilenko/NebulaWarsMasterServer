using System;

namespace AmoebaGameMatcherServer.Services
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