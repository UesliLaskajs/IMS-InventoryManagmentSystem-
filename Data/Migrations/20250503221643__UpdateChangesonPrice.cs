using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS_InventoryManagmentSystem_.Migrations
{
    /// <inheritdoc />
    public partial class _UpdateChangesonPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "Quantity");
        }
    }
}
