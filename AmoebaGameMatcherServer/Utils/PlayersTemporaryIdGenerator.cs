namespace AmoebaGameMatcherServer.Experimental
{
    public static class PlayersTemporaryIdGenerator
    {
        private static int lastPlayerId=777_777;
        public static int GetPlayerId()
        {
            return lastPlayerId++;
        }
    }
}