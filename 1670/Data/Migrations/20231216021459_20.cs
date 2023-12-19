using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1670.Data.Migrations
{
    /// <inheritdoc />
    public partial class _20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Order");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
