using Microsoft.EntityFrameworkCore.Migrations;

namespace WalletPlusApi.Infrastructure.Migrations
{
    public partial class walletIee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "WalletTransactions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_CustomerId",
                table: "WalletTransactions",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletTransactions_Customers_CustomerId",
                table: "WalletTransactions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletTransactions_Customers_CustomerId",
                table: "WalletTransactions");

            migrationBuilder.DropIndex(
                name: "IX_WalletTransactions_CustomerId",
                table: "WalletTransactions");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "WalletTransactions");
        }
    }
}
