namespace AmoebaGameMatcherServer.Experimental
{
    public static class GameRoomIdGenerator
    { 
        static int lastGameRoomNumber = 456;
        public static int CreateGameRoomNumber()
        {
            return lastGameRoomNumber++;
        }
    }
}