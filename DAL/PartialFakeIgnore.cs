namespace DataLayer
{
    // Небольшой ужас, чтобы оно запускалось при отсутствии файла Ignore.cs
    public class BaseGoogleApiProfile : GoogleApiProfile
    {
        public override string Code { get; } = null;
        public override string ClientId { get; } = null;
        public override string ClientSecret { get; } = null;
        public override string RedirectUri { get; } = "http://localhost:53846/Test/Test";
        public override string PackageName { get; } = null;
        public override string GoogleApiData { get; } = null;
    }

    public partial class GoogleApiProfileNewest : BaseGoogleApiProfile
    { }
}

namespace DataLayer.Configuration
{
    public partial class DbConnectionConfig : IDbConnectionConfig
    {
        private readonly string connectionString;
        // Если файл Ignore.cs будет отсутствовать, то отработает этот фейковый конструктор
        public DbConnectionConfig(object databaseFakeObject)
        { }

        public string GetConnectionString()
        {
            return connectionString;
        }
    }
}