using DataLayer;

namespace LibraryForTests
{
    public class SmallAccountDirector : AccountDirector
    {
        public SmallAccountDirector(AccountBuilder builder, ApplicationDbContext dbContext) 
            : base(builder, dbContext)
        {
        }
        
        protected override void ConstructWarships()
        {
            Builder.AddWarship();
        }

        protected override void ConstructMatches()
        {
            Builder.AddMatches(15);
        }

        protected override void ConstructWarshipImprovements()
        {
            Builder.AddWarshipImprovements(45);
        }

        protected override void ConstructWarshipLevel()
        {
            Builder.AddWarshipLevels(5);
        }

        protected override void ConstructLootboxes()
        {
            
        }
    }
}