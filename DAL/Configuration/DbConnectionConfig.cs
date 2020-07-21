using System.Data.Common;
using System.Runtime.InteropServices;

namespace DataLayer.Configuration
{
    public static class DbConnectionConfig
    {
        private static readonly DbConnectionStringBuilder conStrBuilder;
        
        static DbConnectionConfig()
        {
            string name = "R15";
            conStrBuilder = new DbConnectionStringBuilder
            {
                {"User ID", "postgres"},
                {"Password",  DbPassIgnore.DbPass},
                { "Port", 5432 },
                { "Integrated Security", true },
                { "Pooling", true }
            };

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                conStrBuilder.Add("Database", "Prod"+name);
                conStrBuilder.Add("Server", "127.0.0.1");
            }
            else
            {
                conStrBuilder.Add("Database", "Dev"+name);
                conStrBuilder.Add("Server", "65.52.151.136");
            }
        }
        
        public static string GetConnectionString(string databaseName = null)
        {
            if (databaseName != null)
            {
                conStrBuilder.Remove("Database");
                conStrBuilder.Add("Database", databaseName);
            }
            
            return conStrBuilder.ConnectionString;
        }
    }
}