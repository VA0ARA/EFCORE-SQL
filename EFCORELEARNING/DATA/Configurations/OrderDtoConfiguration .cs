using EFCORELEARNING.DTOs;
using EFCORELEARNING.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCORELEARNING.DATA.Configurations
{
    public class OrderDtoConfiguration : IEntityTypeConfiguration<OrderDto>
    {
        // Set up a property of entity  
        public void Configure(EntityTypeBuilder<OrderDto> builder)
        {
            builder.HasNoKey();
            builder.ToView(null);//just for query not for maping data
        }
    }
}
