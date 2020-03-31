using System;

namespace MatchmakerTest.Utils
{
    /// <summary>
    /// Создаёт уникальные строки. Нужно для serviceId в тестах.
    /// </summary>
    public static class UniqueStringFactory
    {
        private static int count;
        public static string Create()
        {
            count++;
            Guid guid = new Guid(count, Int16.MaxValue, Int16.MaxValue, Byte.MaxValue, Byte.MaxValue,
                Byte.MaxValue, Byte.MaxValue, Byte.MaxValue, Byte.MaxValue, Byte.MaxValue, Byte.MaxValue);
            return guid.ToString();
        }
    }
}