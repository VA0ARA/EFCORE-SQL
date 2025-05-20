using EFCORELEARNING.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Reflection;

namespace EFCORELEARNING.DATA.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        // Set up a property of entity  
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //property
            builder.ToTable("Employees");
            builder.HasKey(x => x.EmployeeID);// uniqe-notnull-doesnot be repitation
            builder.Property(x => x.EmployeeID)//not null -generate automaticly 
                   .IsRequired()
                   .UseIdentityColumn()
                   .ValueGeneratedOnAdd();
            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(x => x.Title)
               // .IsRequired()
                .HasMaxLength (30);
            builder.Property(x => x.TitleofCourtesy)
               // .IsRequired()
                .HasMaxLength(25);
            builder.Property(x => x.Birthdate)
                .IsRequired()
                .HasColumnType("datetime");
            builder.Property(x => x.Hiredate)
              .IsRequired()
              .HasColumnType("datetime");
            builder.Property(x => x.City)
                //.IsRequired()
                .HasMaxLength(15);
            builder.Property(x=>x.Region)
                .HasMaxLength(15);
            builder.Property(x => x.State)//can be null
                .HasMaxLength(30);
            builder.Property(x => x.mgrid);// can be null
            //2.seed data with create C#Model
            #region SeedData
            Employee obj1 = new Employee { FirstName = "سحر", LastName = "تقوی", Title = "CEO", TitleofCourtesy = "Ms.", City = "تهران", Region = "مرکز", State = "تهران", mgrid = null, Birthdate = new DateTime(1958, 12, 08, 7, 47, 0), Hiredate = new DateTime(2002, 05, 01, 00, 00, 00) };
            obj1.EmployeeID = 1;
            Employee obj2 = new Employee { FirstName = "بهزاد", LastName = "فکری", Title = "Vice President, Sales", TitleofCourtesy = "Mr.", City = "تهران", Region = "مرکز", State = "تهران", mgrid = 1, Birthdate = new DateTime(1962, 02, 19, 00, 00, 00), Hiredate = new DateTime(2002, 08, 14, 00, 00, 00) };
            obj2.EmployeeID = 2;
            Employee obj3 = new Employee { FirstName = "علی", LastName = "پایدار", Title = "Sales Manager", TitleofCourtesy = "Ms.", City = "تهران", Region = "مرکز", State = "تهران", mgrid = 2, Birthdate = new DateTime(1973, 08, 30, 00, 00, 00), Hiredate = new DateTime(2002, 04, 01, 00, 00, 00) };
            obj3.EmployeeID = 3;
            Employee obj4 = new Employee { FirstName = "زهره", LastName = "تهرانی", Title = "Sales Representative", TitleofCourtesy = "Ms.", City = "پردیس", Region = "حومه", State = "تهران", mgrid = 3, Birthdate = new DateTime(1947, 09, 19, 00, 00, 00), Hiredate = new DateTime(2003, 05, 01, 00, 00, 00) };
            obj4.EmployeeID =4;
            Employee obj5 = new Employee { FirstName = "کامران", LastName = "اسماعیلی", Title = "Sales Manager", TitleofCourtesy = "Mr.", City = "کرج", Region = null, State = "البرز", mgrid = 2, Birthdate = new DateTime(1965, 12, 03, 00, 00, 00), Hiredate = new DateTime(2003, 10, 17, 00, 00, 00) };
            obj5.EmployeeID =5;
            Employee obj6 = new Employee { FirstName = "سعید", LastName = "دهقان", Title = "Sales Representative", TitleofCourtesy = "Mr.", City = "کرج", Region = null, State = "البرز", mgrid = 5, Birthdate = new DateTime(1958, 12, 08, 00, 00, 0), Hiredate = new DateTime(2004, 01, 02, 00, 00, 00) };
            obj6.EmployeeID = 6;
            Employee obj7 = new Employee { FirstName = "پیمان", LastName = "سلامی", Title = "Sales Representative", TitleofCourtesy = null, City = "کرج", Region = null, State = "البرز", mgrid = 5, Birthdate = new DateTime(1970, 05, 29, 00, 00, 00), Hiredate = new DateTime(2002, 05, 01, 00, 00, 00) };
            obj7.EmployeeID = 7;
            Employee obj8 = new Employee { FirstName = "زهرا", LastName = "یکتا", Title = "Sales Representative", TitleofCourtesy = "Ms.", City = "کرج", Region = "مرکز", State = "البرز", mgrid = 3, Birthdate = new DateTime(1968, 01, 09, 7, 00, 00), Hiredate = new DateTime(2004, 03, 11, 7, 00, 00) };
            obj8.EmployeeID = 8;
            Employee obj9 = new Employee { FirstName = "محمد رضا", LastName = "زنده دل", Title = "Sales Representative", TitleofCourtesy = "Mr.", City = "شهریار", Region = null, State = "تهران", mgrid = 5, Birthdate = new DateTime(1976, 01, 27, 7, 00, 00), Hiredate = new DateTime(2004, 11, 15, 00, 00, 00) };
            obj9.EmployeeID =9;
            builder.HasData(new List<Employee>()
                        {
                            obj1,
                            obj2,
                            obj3,
                            obj4,
                            obj5,
                            obj6,
                            obj7,
                            obj8,
                            obj9,
                        });
            #endregion


        }
    }
}
