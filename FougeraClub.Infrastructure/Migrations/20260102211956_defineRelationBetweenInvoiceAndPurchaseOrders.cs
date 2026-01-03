using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FougeraClub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class defineRelationBetweenInvoiceAndPurchaseOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "VATRate",
                table: "invoices",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldDefaultValue: 0.14999999999999999);

            migrationBuilder.CreateIndex(
                name: "IX_invoices_PurchaseOrderId",
                table: "invoices",
                column: "PurchaseOrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_invoices_purchase_Orders_PurchaseOrderId",
                table: "invoices",
                column: "PurchaseOrderId",
                principalTable: "purchase_Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoices_purchase_Orders_PurchaseOrderId",
                table: "invoices");

            migrationBuilder.DropIndex(
                name: "IX_invoices_PurchaseOrderId",
                table: "invoices");

            migrationBuilder.AlterColumn<double>(
                name: "VATRate",
                table: "invoices",
                type: "float",
                nullable: false,
                defaultValue: 0.14999999999999999,
                oldClrType: typeof(double),
                oldType: "float",
                oldDefaultValue: 0.0);
        }
    }
}
