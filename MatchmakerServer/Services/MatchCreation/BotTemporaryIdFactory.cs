using System;

namespace AmoebaGameMatcherServer.Services.MatchCreation
{
    public static class BotTemporaryIdFactory
    {
        private static ushort lastId = (ushort)DateTime.Now.Ticks; // Немного добавляет рандома в начальный идентификатор
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