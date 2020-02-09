namespace AmoebaGameMatcherServer.Experimental
{
    public static class PlayersTemporaryIdGenerator
    {
        private static int lastPlayerId=0;
        public static int GetPlayerId()
        {
            return lastPlayerId++;
        }
    }
}