namespace AmoebaGameMatcherServer.Services.MatchCreation
{
    public static class BotTemporaryIdFactory
    {
        private static ushort lastId=32_000;
        public static ushort Create()
        {
            unchecked
            {
                lastId++;
            }
            return lastId;
        }
    }
}