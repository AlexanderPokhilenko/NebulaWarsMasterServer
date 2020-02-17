namespace AmoebaGameMatcherServer.Utils
{
    public static class PlayersTemporaryIdGenerator
    {
        private static int lastPlayerId = 888_777;
        public static int GetPlayerId()
        {
            return lastPlayerId++;
        }
    }
}