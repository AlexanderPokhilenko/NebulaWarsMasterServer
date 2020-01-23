using System.Collections.Generic;

namespace AmoebaGameMatcherServer.Experimental
{
    public class GameRoomData
    {
        public int GameToomNumber;
        public List<PlayerInfoForGameRoom> Players;
    }

    public class PlayerInfoForGameRoom
    {
        public string PlayerLogin;
        public bool IsBot;
    }
}