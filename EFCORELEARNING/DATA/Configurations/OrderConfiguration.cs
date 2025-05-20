using EFCORELEARNING.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCORELEARNING.DATA.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        // Set up a property of entity  
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //property
            builder.ToTable("Orders");
            builder.HasKey(x => x.OrderID);// uniqe-notnull-doesnot be repitation
            builder.Property(x => x.OrderID)//not null -generate automaticly 
                   .IsRequired();
                   //.UseIdentityColumn()// increase automatecly 
                   //.ValueGeneratedOnAdd();
            builder.Property(x => x.ShipperID)
                   .HasColumnType("int");
            builder.Property(x => x.OrderDate)
                .IsRequired()
                .HasColumnType("datetime");
            builder.Property(x => x.Freight)
                .IsRequired()
                .HasColumnType("money");


            //navigate Property
            //rel Customer-Order 1-m
            builder.HasOne(o => o.CustomerOfOrder)
                   .WithMany(c => c.OrdersOfCustomer)
                   .HasForeignKey(o => o.CustomerID)
                    .OnDelete(DeleteBehavior.Restrict);
            //rel Employee-Order 1-m
            builder.HasOne(o => o.EmployeeOfOrder)
                  .WithMany(c => c.OrdersOfEmployee)
                  .HasForeignKey(o => o.EmployeeID)
                  .OnDelete(DeleteBehavior.Restrict);
            // .OnDelete(DeleteBehavior.Restrict) // do not allow to delete parent  
            //.OnDelete(DeleteBehavior.Cascade)   // delete child when parent delete 
            //.OnDelete(DeleteBehavior.SetNull)   // set null when parent delete  
        }
    }
}
