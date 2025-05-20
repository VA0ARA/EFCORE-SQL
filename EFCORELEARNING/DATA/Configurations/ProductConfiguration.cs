using EFCORELEARNING.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCORELEARNING.DATA.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        // Set up a property of entity  
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //property
            builder.ToTable("Products");
            builder.HasKey(x => x.ProductID);// uniqe-notnull-doesnot be repitation
            builder.Property(x => x.ProductID)//not null -generate automaticly 
                   .IsRequired()
                   .UseIdentityColumn()
                   .ValueGeneratedOnAdd();
            builder.Property(x => x.ProductName)
                   .IsRequired()
                   .HasColumnType("nvarchar(500)");
            builder.Property(x => x.SupplierID)
                .IsRequired()
                .HasColumnType("int");
            builder.Property(x => x.CategoryID)
                .IsRequired()
                .HasColumnType("int");
            builder.Property(x => x.UnitPrice)
                .IsRequired()
                .HasColumnType("money");
            builder.Property(x=>x.Discontinued)
                .IsRequired()
                .HasColumnType ("bit");


            //navigate Property
            //EmployeeID
            //CustomerID


        }
    }
}
