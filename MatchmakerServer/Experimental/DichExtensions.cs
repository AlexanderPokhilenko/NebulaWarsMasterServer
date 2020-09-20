using System;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Experimental
{
    public static class DichExtensions
    {
        /// <summary>
        /// Я не смог найти другой способ отправлять массивы байт по http
        /// </summary>
        /// <param name="zeroFormattable"></param>
        /// <returns></returns>
        public static string SerializeToBase64String<T>(this T zeroFormattable)
        {
            byte[] data = ZeroFormatterSerializer.Serialize(zeroFormattable);
            string base64 = Convert.ToBase64String(data);
            return base64;
        }
    }
}