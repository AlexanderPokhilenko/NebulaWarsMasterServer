using DataLayer;

namespace LibraryForTests
{
    public class MediumAccountDirector:AccountDirector
    {
        public MediumAccountDirector(AccountBuilder builder, ApplicationDbContext dbContext) 
            : base(builder, dbContext)
        {
        }
        
        protected override void ConstructWarships()
        {
            Builder.AddWarship();
            Builder.AddWarship();
            Builder.AddWarship();
        }

        protected override void ConstructLootboxes()
        {
            Builder.AddLootbox(1,1,1, false);
            Builder.AddLootbox(3,0,0, false);
            Builder.AddLootbox(2,2,0, true);
        }

        protected override void ConstructMatches()
        {
            Builder.AddMatches(51);
        }

        protected override void ConstructWarshipImprovements()
        {
            Builder.AddWarshipImprovements(12);
        }

        protected override void ConstructWarshipLevel()
        {
            Builder.AddWarshipLevels(12);
        }
    }
}