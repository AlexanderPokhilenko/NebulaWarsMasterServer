using Newtonsoft.Json;

namespace AmoebaGameMatcherServer.Services.GoogleApi
{
    public class GoogleResponse
    {
        [JsonProperty("obfuscatedExternalAccountId")]
        public string ObfuscatedExternalAccountId;
    }
}