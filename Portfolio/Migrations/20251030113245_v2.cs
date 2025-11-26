using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetValue",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "InvestedValue",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "Orders",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "currentPrice",
                table: "Orders",
                newName: "CurrentPrice");

            migrationBuilder.RenameColumn(
                name: "buyPrice",
                table: "Orders",
                newName: "BuyPrice");

            migrationBuilder.RenameColumn(
                name: "Profit",
                table: "Orders",
                newName: "InvestedAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Orders",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "CurrentPrice",
                table: "Orders",
                newName: "currentPrice");

            migrationBuilder.RenameColumn(
                name: "BuyPrice",
                table: "Orders",
                newName: "buyPrice");

            migrationBuilder.RenameColumn(
                name: "InvestedAmount",
                table: "Orders",
                newName: "Profit");

            migrationBuilder.AddColumn<double>(
                name: "AssetValue",
                table: "Orders",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InvestedValue",
                table: "Orders",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
