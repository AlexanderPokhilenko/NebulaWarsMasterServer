using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.TablesConfiguration
{
    public class ResourcesConfiguration:IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder
                .HasOne(prod => prod.Transaction)
                .WithMany(order => order.Resources)
                .HasForeignKey(prod => prod.TransactionId);
            
            builder
                .HasOne(prod => prod.ResourceType)
                .WithMany(productType => productType.Products)
                .HasForeignKey(prod => prod.ResourceTypeId);
        }
    }
}