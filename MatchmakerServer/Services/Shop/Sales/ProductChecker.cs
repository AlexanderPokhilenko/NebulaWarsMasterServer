using System;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services.Shop.Sales
{
    public class ProductChecker
    {
        public bool IsEqual(ProductModel productModel1, ProductModel productModel2)
        {
            if (productModel1.Id != productModel2.Id)
            {
                // Console.WriteLine("Id");
                return false;
            }

            // Console.WriteLine(productModel1.Id+" "+productModel2.Id);

            if (productModel1.ResourceTypeEnum != productModel2.ResourceTypeEnum)
            {
                // Console.WriteLine("ResourceTypeEnum");
                return false;
            }

            // Console.WriteLine(productModel1.ResourceTypeEnum+" "+productModel2.ResourceTypeEnum);

            if (productModel1.SerializedModel.Length != productModel2.SerializedModel.Length)
            {
                // Console.WriteLine("SerializedModel.Length");
                return false;
            }

            // Console.WriteLine(productModel1.SerializedModel.Length+" "+productModel2.SerializedModel.Length);

            switch (productModel1.ResourceTypeEnum)
            {
                case ResourceTypeEnum.WarshipPowerPoints:
                {
                    
                
                    var model1 = ZeroFormatterSerializer.Deserialize<WarshipPowerPointsProductModel>(productModel1.SerializedModel);
                    var model2 = ZeroFormatterSerializer.Deserialize<WarshipPowerPointsProductModel>(productModel2.SerializedModel);
                    
                    if (model1.Increment!=model2.Increment)
                    {
                        // Console.WriteLine("FinishValue ");
                        return false;
                    }
                    if (model1.WarshipId!=model2.WarshipId)
                    {
                        // Console.WriteLine("WarshipId");
                        return false;
                    }
                    break;
                }
                case ResourceTypeEnum.SoftCurrency:
                {
                    var model1 = ZeroFormatterSerializer.Deserialize<SoftCurrencyProductModel>(productModel1.SerializedModel);
                    var model2 = ZeroFormatterSerializer.Deserialize<SoftCurrencyProductModel>(productModel2.SerializedModel);

                    if (model1.Amount != model2.Amount)
                    {
                        return false;
                    }
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            
            if (productModel1.CostModel.CostTypeEnum != productModel2.CostModel.CostTypeEnum)
            {
                // Console.WriteLine("CostTypeEnum");
                return false;
            }

            // Console.WriteLine(productModel1.SerializedModel.Length+" "+productModel2.SerializedModel.Length);

            if (productModel1.CostModel.SerializedCostModel.Length != productModel2.CostModel.SerializedCostModel.Length)
            {
                // Console.WriteLine("CostModel.SerializedCostModel.Length");
                return false;
            }

            // Console.WriteLine(productModel1.CostModel.SerializedCostModel.Length+" "+productModel2.CostModel.SerializedCostModel.Length);

            if (productModel1.ProductSizeEnum != productModel2.ProductSizeEnum)
            {
                // Console.WriteLine("ProductSizeEnum");
                return false;
            }

            // Console.WriteLine(productModel1.ProductSizeEnum+" "+productModel2.ProductSizeEnum);

            if (productModel1.IsDisabled != productModel2.IsDisabled)
            {
                // Console.WriteLine("IsDisabled");
                return false;
            }

            // Console.WriteLine(productModel1.IsDisabled+" "+productModel2.IsDisabled);

            if (productModel1.ProductMark != productModel2.ProductMark)
            {
                // Console.WriteLine("ProductMark");
                return false;
            }

            // Console.WriteLine(productModel1.SerializedModel.Length+" "+productModel2.SerializedModel.Length);
            if (productModel1.ProductMark?.ProductMarkTypeEnum != productModel2.ProductMark?.ProductMarkTypeEnum)
            {
                // Console.WriteLine("ProductMark.ProductMarkTypeEnum");
                return false;
            }

            // Console.WriteLine(productModel1.ProductMark?.ProductMarkTypeEnum+" "+productModel2.ProductMark?.ProductMarkTypeEnum);

            if (productModel1.ProductMark?.SerializedProductMark.Length != productModel2.ProductMark?.SerializedProductMark.Length)
            {
                // Console.WriteLine("ProductMark.SerializedProductMark.Length");
                return false;
            }

            // Console.WriteLine(productModel1.ProductMark?.SerializedProductMark.Length+" "+productModel2.ProductMark?.SerializedProductMark.Length);

            if (productModel1.PreviewImagePath != productModel2.PreviewImagePath)
            {
                // Console.WriteLine("PreviewImagePath");
                return false;
            }

            // Console.WriteLine(productModel1.PreviewImagePath+" "+productModel2.PreviewImagePath);


            // Console.WriteLine("Проверка прошла успешно");
            return true;
        }
    }
}