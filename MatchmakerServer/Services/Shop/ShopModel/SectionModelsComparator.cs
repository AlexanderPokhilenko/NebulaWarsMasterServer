using System.Collections.Generic;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Shop.ShopModel
{
    /// <summary>
    /// Нужен для того, чтобы понять нужноли обновить модель магазина в БД.
    /// </summary>
    public class SectionModelsComparator
    {
        public bool NeedToReplace(List<SectionModel> sections1, List<SectionModel> sections2)
        {
            if (sections1.Count != sections2.Count)
            {
                return true;
            }

            for (int i = 0; i < sections1.Count ; i++)
            {
                var section1 = sections1[i];
                var section2 = sections2[i];
                if (NeedToReplace(section1, section2))
                {
                    return true;
                }
            }

            return false;
        }
        private bool NeedToReplace(SectionModel sectionModel1,  SectionModel sectionModel2)
        {
            //Проверить кол-во товаров
            if (sectionModel1.ProductsCount() != sectionModel2.ProductsCount())
            {
                return true;
            }

            //проверить тип ресурса
            for (int i = 0; i < sectionModel1.ProductsCount(); i++)
            {
                var model1 = sectionModel1.GetProduct(i);
                var model2 = sectionModel2.GetProduct(i);

                if (model1.ResourceTypeEnum != model2.ResourceTypeEnum)
                {
                    return true;
                }
            }

            return false;
        }
    }
}