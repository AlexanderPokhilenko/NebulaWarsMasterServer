using DataLayer;

namespace AmoebaGameMatcherServer
{
    public class DataSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            new GameModeSeeder().Seed(dbContext);
            new WarshipCombatRoleSeeder().Seed(dbContext);
            new WarshipTypesSeeder().Seed(dbContext);
            new TransactionTypesSeeder().Seed(dbContext);
            new IncrementTypeSeeder().Seed(dbContext);
            new MatchRewardTypeSeeder().Seed(dbContext);
            new DecrementTypeSeeder().Seed(dbContext);
            new AccountSeeder().Seed(dbContext);
            new SkinTypesSeeder().Seed(dbContext);
        }
    }
}