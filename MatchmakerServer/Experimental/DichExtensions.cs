using System;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Controllers
{
    public static class DichExtensions
    {
        /// <summary>
        /// Я не смог найти метод отправлять массивы байтов по http кроме этого метода
        /// </summary>
        /// <param name="zeroFormattable"></param>
        /// <returns></returns>
        public static string SerializeToBase64String<T>(this T zeroFormattable)
        {
            byte[] data = ZeroFormatterSerializer.Serialize(zeroFormattable);
            string base64Dich = Convert.ToBase64String(data);
            return base64Dich;
        }
    }
}