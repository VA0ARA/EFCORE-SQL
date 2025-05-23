using EFCORELEARNING.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCORELEARNING.DATA.Configurations
{
    public class OrderIndexConfiguration : IEntityTypeConfiguration<Order>
    {
        // Set up a property of entity  
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderID);

            builder.HasIndex(o => o.OrderID)
                   .HasDatabaseName("IX_Order_OrderID");

            builder.HasIndex(o => o.CustomerID)
                   .HasDatabaseName("IX_Order_CustomerID");

            builder.HasIndex(o => o.OrderDate)
                   .HasDatabaseName("IX_Order_OrderDate");
        }
    }
}
