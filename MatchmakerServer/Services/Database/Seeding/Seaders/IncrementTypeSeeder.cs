using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services.Database.Seeding.Seaders
{
    public class IncrementTypeSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.IncrementTypes.Any())
            {
                var incrementTypes = new List<IncrementType>
                {
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.Warship.ToString(),
                        Id = IncrementTypeEnum.Warship
                    },
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.Skin.ToString(), 
                        Id = IncrementTypeEnum.Skin
                    },
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.SoftCurrency.ToString(),
                        Id = IncrementTypeEnum.SoftCurrency
                    },
                    
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.HardCurrency.ToString(),
                        Id = IncrementTypeEnum.HardCurrency
                    },
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.WarshipPowerPoints.ToString(),
                        Id = IncrementTypeEnum.WarshipPowerPoints
                    },
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.WarshipLevel.ToString(),
                        Id = IncrementTypeEnum.WarshipLevel
                    },
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.LootboxPoints.ToString(),
                        Id = IncrementTypeEnum.LootboxPoints
                    }
                    ,
                    new IncrementType
                    {
                        Name = IncrementTypeEnum.WarshipRating.ToString(),
                        Id = IncrementTypeEnum.WarshipRating
                    }
                };
                dbContext.IncrementTypes.AddRange(incrementTypes);
                dbContext.SaveChanges();
            }
            
            if (dbContext.IncrementTypes.Count() != Enum.GetNames(typeof(IncrementTypeEnum)).Length)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}