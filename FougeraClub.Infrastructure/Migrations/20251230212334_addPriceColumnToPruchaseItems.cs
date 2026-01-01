using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FougeraClub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addPriceColumnToPruchaseItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PricePerUnit",
                table: "purchase_Items",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                table: "purchase_Items");
        }
    }
}
