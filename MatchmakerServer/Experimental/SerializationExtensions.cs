using System;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Experimental
{
    public static class SerializationExtensions
    {
        public static string SerializeToBase64String<T>(this T zeroFormattable)
        {
            byte[] data = ZeroFormatterSerializer.Serialize(zeroFormattable);
            string base64 = Convert.ToBase64String(data);
            return base64;
        }
    }
}