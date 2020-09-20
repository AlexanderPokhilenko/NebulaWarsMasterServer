namespace DataLayer
{
    public interface IDbContextFactory
    {
        ApplicationDbContext Create(string databaseName=null);
    }
}