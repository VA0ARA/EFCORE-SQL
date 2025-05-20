using EFCORELEARNING.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCORELEARNING.DATA.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        // Set up a property of entity  
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            //property
            builder.ToTable("OrderDetails");
            builder.HasKey(od => new { od.OrderID, od.ProductID });// Composite Primary Key- does not auto-increment-not null-uninq
            builder.Property(x => x.UnitPrice)
                   .HasColumnType("money");
            builder.Property(x => x.Qty)
                   .HasColumnType("smallint");
            builder.Property(x => x.Discount)
                    .HasColumnType("numeric(4,3)");
            //Rel-navigation
            builder.HasOne(od => od.OrderOfOrderDetail)
                    .WithMany(o => o.ListOfOrderDetailsOfOrder)
                    .HasForeignKey(od => od.OrderID);

            builder.HasOne(od => od.ProductOfOrderDetail)
                   .WithMany(p => p.ListOfOrderDetailsOfProduct)
                   .HasForeignKey(od => od.ProductID);
        }
    }
}
