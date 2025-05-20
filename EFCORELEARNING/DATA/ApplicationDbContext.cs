using EFCORELEARNING.DATA.Configurations;
using EFCORELEARNING.DTOs;
using EFCORELEARNING.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace EFCORELEARNING.DATA
{
    public class ApplicationDbContext:DbContext //heart of EF >> set conection with data base >> CRUD-Migration-and....
    {
        //1.get setup conection from program.cs 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        { 

        }
        //2.Set property for every attribute taht configure in the diffrent class 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.ApplyConfiguration(new CustomerConfiguration());
             modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
             modelBuilder.ApplyConfiguration(new OrderConfiguration());
             modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
             modelBuilder.ApplyConfiguration(new ProductConfiguration());
             modelBuilder.ApplyConfiguration(new OrderDtoConfiguration());
            

            base.OnModelCreating(modelBuilder);
        }
        //3. set DBSET that generate Table 
        public DbSet<EFCORELEARNING.Models.Customer> Customers { get; set; }
        public DbSet<EFCORELEARNING.Models.Employee> Employees { get; set; }
        public DbSet<EFCORELEARNING.Models.Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        // just for query
        public DbSet<OrderDto> OrderDtos { get; set; }
        public DbSet<Product> Products { get; set; }
        //1. Seedata >> with Gsoon File In Program.cs





    }
}
