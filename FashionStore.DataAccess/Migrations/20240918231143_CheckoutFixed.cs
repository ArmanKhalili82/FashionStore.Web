using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CheckoutFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "orders");
        }
    }
}
