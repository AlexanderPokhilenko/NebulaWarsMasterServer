using DataLayer;

namespace AmoebaGameMatcherServer
{
    public class DataSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            new WarshipCombatRoleSeeder().Seed(dbContext);
            new WarshipTypesSeeder().Seed(dbContext);
            new OrderTypesSeeder().Seed(dbContext);
            new ProductTypesSeeder().Seed(dbContext);
            new IncrementTypeSeeder().Seed(dbContext);
            new DecrementTypeSeeder().Seed(dbContext);
            
            new AccountSeeder().Seed(dbContext);
        }
    }
}