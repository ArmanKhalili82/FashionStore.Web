using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SalesCount",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrderNo",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "products");

            migrationBuilder.DropColumn(
                name: "SalesCount",
                table: "products");

            migrationBuilder.DropColumn(
                name: "OrderNo",
                table: "orders");
        }
    }
}
