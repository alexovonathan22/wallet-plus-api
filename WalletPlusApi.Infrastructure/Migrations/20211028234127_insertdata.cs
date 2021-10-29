using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WalletPlusApi.Infrastructure.Migrations
{
    public partial class insertdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MoneyWallets",
                columns: new[] { "Id", "BalanceAmount", "CreatedAt", "CustomerId", "DeletedAt", "IsActive", "IsDeleted", "IsVerified", "LastUpdated", "WalletId" },
                values: new object[] { 1L, 0.00m, new DateTime(2021, 10, 29, 0, 41, 27, 277, DateTimeKind.Local).AddTicks(8833), 1L, null, false, false, false, new DateTime(2021, 10, 29, 0, 41, 27, 278, DateTimeKind.Local).AddTicks(6439), "123456778" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MoneyWallets",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
