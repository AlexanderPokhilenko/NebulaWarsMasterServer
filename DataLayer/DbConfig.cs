using System.Data.Common;
using System.Runtime.InteropServices;

namespace DataLayer
{
    public class DbConfig
    {
        static DbConfig()
        {
          
        }

        private static readonly string ConnectionString;
        
        public string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}