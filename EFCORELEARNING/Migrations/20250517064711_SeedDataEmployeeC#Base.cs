using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCORELEARNING.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataEmployeeCBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "Birthdate", "City", "FirstName", "Hiredate", "LastName", "Region", "State", "Title", "TitleofCourtesy", "mgrid" },
                values: new object[,]
                {
                    { 1, new DateTime(1958, 12, 8, 7, 47, 0, 0, DateTimeKind.Unspecified), "تهران", "سحر", new DateTime(2002, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تقوی", "مرکز", "تهران", "CEO", "Ms.", null },
                    { 2, new DateTime(1962, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهران", "بهزاد", new DateTime(2002, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "فکری", "مرکز", "تهران", "Vice President, Sales", "Mr.", 1 },
                    { 3, new DateTime(1973, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهران", "علی", new DateTime(2002, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "پایدار", "مرکز", "تهران", "Sales Manager", "Ms.", 2 },
                    { 4, new DateTime(1947, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "پردیس", "زهره", new DateTime(2003, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تهرانی", "حومه", "تهران", "Sales Representative", "Ms.", 3 },
                    { 5, new DateTime(1965, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "کرج", "کامران", new DateTime(2003, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "اسماعیلی", null, "البرز", "Sales Manager", "Mr.", 2 },
                    { 6, new DateTime(1958, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "کرج", "سعید", new DateTime(2004, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "دهقان", null, "البرز", "Sales Representative", "Mr.", 5 },
                    { 7, new DateTime(1970, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "کرج", "پیمان", new DateTime(2002, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "سلامی", null, "البرز", "Sales Representative", null, 5 },
                    { 8, new DateTime(1968, 1, 9, 7, 0, 0, 0, DateTimeKind.Unspecified), "کرج", "زهرا", new DateTime(2004, 3, 11, 7, 0, 0, 0, DateTimeKind.Unspecified), "یکتا", "مرکز", "البرز", "Sales Representative", "Ms.", 3 },
                    { 9, new DateTime(1976, 1, 27, 7, 0, 0, 0, DateTimeKind.Unspecified), "شهریار", "محمد رضا", new DateTime(2004, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "زنده دل", null, "تهران", "Sales Representative", "Mr.", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 9);
        }
    }
}
