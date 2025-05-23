using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCORELEARNING.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexesForCustomerAndOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                newName: "IX_Order_CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderDate",
                table: "Orders",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderID",
                table: "Orders",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_City",
                table: "Customers",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CurtomerId",
                table: "Customers",
                column: "CurtomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Region",
                table: "Customers",
                column: "Region");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Order_OrderDate",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Customer_City",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customer_CurtomerId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customer_Region",
                table: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerID",
                table: "Orders",
                newName: "IX_Orders_CustomerID");
        }
    }
}
