using System.Data.Common;
using System.Runtime.InteropServices;

namespace DataLayer.Configuration
{
    public static class DbConnectionConfig
    {
        private static readonly DbConnectionStringBuilder ConStrBuilder;
        
        static DbConnectionConfig()
        {
            string name = "R27";
            ConStrBuilder = new DbConnectionStringBuilder
            {
                {"User ID", "postgres"},
                {"Password",  DbPassIgnore.DbPass},
                { "Port", 5432 },
                { "Integrated Security", true },
                { "Pooling", true }
            };

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                ConStrBuilder.Add("Database", "Prod"+name);
                ConStrBuilder.Add("Server", "127.0.0.1");
            }
            else
            {
                ConStrBuilder.Add("Database", "Dev"+name);
                ConStrBuilder.Add("Server", "tikaytech.games");
            }
        }
        
        public static string GetConnectionString(string databaseName = null)
        {
            if (databaseName != null)
            {
                ConStrBuilder.Remove("Database");
                ConStrBuilder.Add("Database", databaseName);
            }
            
            return ConStrBuilder.ConnectionString;
        }
    }
}