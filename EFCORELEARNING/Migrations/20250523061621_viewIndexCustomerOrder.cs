using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCORELEARNING.Migrations
{
    /// <inheritdoc />
    public partial class viewIndexCustomerOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        CREATE VIEW dbo.CustomerOrderIndex
        WITH SCHEMABINDING
        AS
        SELECT 
            c.CurtomerId,
            COUNT_BIG(*) AS OrderCount
        FROM dbo.Customers c
        INNER JOIN dbo.Orders o ON c.CurtomerId = o.CustomerID
        GROUP BY c.CurtomerId;
    ");
            migrationBuilder.Sql("CREATE UNIQUE CLUSTERED INDEX IX_CustomerOrderIndex ON dbo.CustomerOrderIndex(CurtomerId);");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.CustomerOrderIndex;");
        }
    }
}
