namespace DataLayer
{
    public interface IDbContextFactory
    {
        ApplicationDbContext Create();
    }
}