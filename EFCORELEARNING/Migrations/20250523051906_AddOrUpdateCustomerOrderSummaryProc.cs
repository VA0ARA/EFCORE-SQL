using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCORELEARNING.Migrations
{
    /// <inheritdoc />
    public partial class AddOrUpdateCustomerOrderSummaryProc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
            IF OBJECT_ID('GetCustomersWithMoreThanFiveOrderDetails', 'P') IS NOT NULL
                DROP PROCEDURE GetCustomersWithMoreThanFiveOrderDetails;
        ");


            migrationBuilder.Sql(@"
            CREATE PROCEDURE GetCustomersWithMoreThanFiveOrderDetails
            AS
            BEGIN
                SELECT Tmp.CustomerID, Tmp.Num
                FROM
                (
                    SELECT
                        O.CustomerID,
                        (SELECT COUNT(OD.OrderID)
                         FROM dbo.OrderDetails AS OD
                         WHERE O.OrderID = OD.OrderID) AS Num
                    FROM dbo.Orders AS O
                    GROUP BY O.CustomerID, O.OrderID
                ) AS Tmp
                WHERE Tmp.Num > 3
                GROUP BY Tmp.CustomerID, Tmp.Num;
            END
        ");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        IF OBJECT_ID('GetCustomersWithMoreThanFiveOrderDetails', 'P') IS NOT NULL
            DROP PROCEDURE GetCustomersWithMoreThanFiveOrderDetails;
    ");

        }
    }
}
