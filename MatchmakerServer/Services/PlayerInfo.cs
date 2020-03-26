using System;
using System.Collections.Concurrent;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    public class PlayerInfo
    {
        public string PlayerId;
        public DateTime DictionaryEntryTime;
        public WarshipCopy WarshipCopy;
    }
}