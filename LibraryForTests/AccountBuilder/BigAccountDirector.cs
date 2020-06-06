using System;
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
    
    public class MediumAccountDirector:AccountDirector
    {
        public MediumAccountDirector(AccountBuilder builder, ApplicationDbContext dbContext) 
            : base(builder, dbContext)
        {
        }
        
        protected override void ConstructWarships()
        {
            Builder.AddWarship(1);
        }

        protected override void ConstructLootboxes()
        {
            Builder.AddLootbox(1,1,1, false);
            Builder.AddLootbox(3,0,0, false);
            Builder.AddLootbox(2,2,0, true);
        }
    }
    public class BigAccountDirector:AccountDirector
    {
        public BigAccountDirector(AccountBuilder builder, ApplicationDbContext dbContext)
            : base(builder, dbContext)
        {
        }
        
        protected override void ConstructWarships()
        {
            Builder.AddWarship(5);
            Builder.AddWarship(13);
        }

        protected override void ConstructLootboxes()
        {
            Builder.AddLootbox(1,1,1, false);
            Builder.AddLootbox(3,0,0, false);
            Builder.AddLootbox(2,2,0, true);
        }
    }
}