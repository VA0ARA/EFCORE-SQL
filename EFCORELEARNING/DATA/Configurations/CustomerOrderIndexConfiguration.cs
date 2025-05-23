using EFCORELEARNING.DTOs;
using EFCORELEARNING.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCORELEARNING.DATA.Configurations
{
    public class CustomerOrderIndexConfiguration : IEntityTypeConfiguration<CustomerOrderIndex>
    {
        // Set up a property of entity  
        public void Configure(EntityTypeBuilder<CustomerOrderIndex> builder)
        {
            builder.HasNoKey(); 
            builder.ToView("CustomerOrderIndex"); 
            builder.Property(v => v.CurtomerId).HasColumnName("CurtomerId");
            builder.Property(v => v.OrderCount).HasColumnName("OrderCount");
            
        }
    }
}
