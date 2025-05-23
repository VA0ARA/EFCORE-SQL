using EFCORELEARNING.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCORELEARNING.DATA.Configurations
{
    public class CustomerIndexConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.CurtomerId);

            builder.HasIndex(c => c.CurtomerId)
                   .HasDatabaseName("IX_Customer_CurtomerId");

            builder.HasIndex(c => c.City)
                   .HasDatabaseName("IX_Customer_City");

            builder.HasIndex(c => c.Region)
                   .HasDatabaseName("IX_Customer_Region");
        }
    }
}
