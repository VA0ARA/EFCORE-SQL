using EFCORELEARNING.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCORELEARNING.DATA.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        // Set up a property of entity  
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            //property
            builder.ToTable("Customers");
            builder.HasKey(x => x.CurtomerId);// uniqe-notnull-doesnot be repitation
            builder.Property(x => x.CurtomerId)//not null -generate automaticly 
                   .IsRequired()
                   .UseIdentityColumn()
                   .ValueGeneratedOnAdd();
            builder.Property(x => x.CompanyName)
                .IsRequired()
                .HasMaxLength(40);
            builder.Property(x => x.ContactName)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(x => x.ContactTitle)
                .IsRequired()
                .HasMaxLength (30);
            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(15);
            builder.Property(x=>x.Region)
                .HasMaxLength(15);
            builder.Property(x => x.State)
                .IsRowVersion()
                .HasMaxLength(30);
        }
    }
}
