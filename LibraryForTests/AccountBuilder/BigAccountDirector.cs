using System;

namespace LibraryForTests
{
    public class SmallAccountDirector : AccountDirector
    {
        public SmallAccountDirector(AccountBuilder builder) 
            : base(builder)
        {
        }

        public override void ConstructStart()
        {
            string username = "username_" + DateTime.Now.ToLongTimeString();
            string serviceId = "serviceId_" + DateTime.Now.ToLongTimeString();
            Builder.SetBaseInfo(username, serviceId, DateTime.Now);
            Builder.AddWarship(5);
        }

        public override void ConstructEnd()
        {
            
        }
    }
    
    public class MediumAccountDirector:AccountDirector
    {
        public MediumAccountDirector(AccountBuilder builder) : base(builder)
        {
        }
        
        public override void ConstructStart()
        {
            string username = "username_" + DateTime.Now.ToLongTimeString();
            string serviceId = "serviceId_" + DateTime.Now.ToLongTimeString();
            Builder.SetBaseInfo(username, serviceId, DateTime.Now);
            Builder.AddWarship(1);
        }

        public override void ConstructEnd()
        {
            Builder.AddLootbox(1,1,1, false);
            Builder.AddLootbox(3,0,0, false);
            Builder.AddLootbox(2,2,0, true);
        }
        
    }
    public class BigAccountDirector:AccountDirector
    {
        public BigAccountDirector(AccountBuilder builder) : base(builder)
        {
        }
        
        public override void ConstructStart()
        {
            string username = "username_" + DateTime.Now.ToLongTimeString();
            string serviceId = "serviceId_" + DateTime.Now.ToLongTimeString();
            Builder.SetBaseInfo(username, serviceId, DateTime.Now);
            Builder.AddWarship(5);
            Builder.AddWarship(13);
            //TODO добавить другие реализации с разным кол-во кораблей
        }

        public override void ConstructEnd()
        {
            Builder.AddLootbox(1,1,1, false);
            Builder.AddLootbox(3,0,0, false);
            Builder.AddLootbox(2,2,0, true);
        }
    }
}