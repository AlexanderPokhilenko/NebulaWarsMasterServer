using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer
{
    public class SkinTypesSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.SkinTypes.Any())
            {
                List<SkinType> skinTypes = new List<SkinType>
                {
                    new SkinType
                    {
                        Name = SkinTypeEnum.Hare.ToString(),
                        Id = SkinTypeEnum.Hare,
                        WarshipTypeId = WarshipTypeEnum.Hare
                    },
                    new SkinType
                    {
                        Name = SkinTypeEnum.Bird.ToString(),
                        Id = SkinTypeEnum.Bird,
                        WarshipTypeId = WarshipTypeEnum.Bird
                    },
                    new SkinType
                    {
                        Name = SkinTypeEnum.Smiley.ToString(),
                        Id = SkinTypeEnum.Smiley,
                        WarshipTypeId = WarshipTypeEnum.Smiley
                    },
                    new SkinType
                    {
                        Name = SkinTypeEnum.HareDestroyer.ToString(),
                        Id = SkinTypeEnum.HareDestroyer,
                        WarshipTypeId = WarshipTypeEnum.Hare
                    },
                };
                dbContext.SkinTypes.AddRange(skinTypes);
                dbContext.SaveChanges();
            }
            
            if (dbContext.SkinTypes.Count() != Enum.GetNames(typeof(SkinTypeEnum)).Length)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}