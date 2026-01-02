using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FougeraClub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceEntityAndUpdatePurchaseitems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "purchase_Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TotalInLine",
                table: "purchase_Items",
                type: "float",
                nullable: false,
                computedColumnSql: "[Amount] * [PricePerUnit]",
                stored: true);

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<double>(type: "float", nullable: false),
                    VATAmount = table.Column<double>(type: "float", nullable: false, computedColumnSql: "[VATRate] * [SubTotal]", stored: true),
                    VATRate = table.Column<double>(type: "float", nullable: false, defaultValue: 0.14999999999999999),
                    TotalAmount = table.Column<double>(type: "float", nullable: false, computedColumnSql: "[SubTotal] + [VATRate] * [SubTotal]", stored: true),
                    IsAsign = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_purchase_Items_InvoiceId",
                table: "purchase_Items",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_Items_invoices_InvoiceId",
                table: "purchase_Items",
                column: "InvoiceId",
                principalTable: "invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchase_Items_invoices_InvoiceId",
                table: "purchase_Items");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropIndex(
                name: "IX_purchase_Items_InvoiceId",
                table: "purchase_Items");

            migrationBuilder.DropColumn(
                name: "TotalInLine",
                table: "purchase_Items");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "purchase_Items");
        }
    }
}
