using EFCORELEARNING.DTOs;
using EFCORELEARNING.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCORELEARNING.DATA.Configurations
{
    public class CustomerOrderDetailDtoConfiguration : IEntityTypeConfiguration<CustomerOrderDetailDto>
    {
        // Set up a property of entity  
        public void Configure(EntityTypeBuilder<CustomerOrderDetailDto> builder)
        {
            builder.HasNoKey();
            builder.ToView(null);//just for query not for maping data
        }
    }
}
