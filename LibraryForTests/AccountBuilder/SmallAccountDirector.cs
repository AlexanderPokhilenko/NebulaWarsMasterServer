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
            Builder.AddWarship(5);
        }

        protected override void ConstructLootboxes()
        {
            
        }
    }
}