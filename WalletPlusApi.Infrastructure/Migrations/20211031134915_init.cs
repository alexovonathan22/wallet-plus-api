using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WalletPlusApi.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AuthToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncryptionKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoneyWallets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BalanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WalletId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencySymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyWallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyWallets_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountAdded = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsWithdrawal = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopUpMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeneficiaryId = table.Column<long>(type: "bigint", nullable: false),
                    DepositorId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PointWallets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PointEarned = table.Column<int>(type: "int", nullable: false),
                    MoneyValueForPointEarned = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointWallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointWallets_MoneyWallets_Id",
                        column: x => x.Id,
                        principalTable: "MoneyWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AuthToken", "CreatedAt", "DeletedAt", "Email", "EncryptionKey", "FirstName", "IsActive", "IsDeleted", "IsVerified", "LastName", "LastUpdated", "OTP", "PasswordHash", "PasswordSalt", "PhoneNumber", "RefreshToken", "SecretKey" },
                values: new object[] { 1L, null, new DateTime(2021, 10, 31, 14, 49, 14, 592, DateTimeKind.Local).AddTicks(7170), null, "great@gmail.com", null, "ofe", false, false, false, "troy", new DateTime(2021, 10, 31, 14, 49, 14, 593, DateTimeKind.Local).AddTicks(4881), null, null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "MoneyWallets",
                columns: new[] { "Id", "BalanceAmount", "CreatedAt", "CurrencySymbol", "CustomerId", "DeletedAt", "IsActive", "IsDeleted", "IsVerified", "LastUpdated", "WalletId" },
                values: new object[] { 1L, 0.00m, new DateTime(2021, 10, 31, 14, 49, 14, 594, DateTimeKind.Local).AddTicks(6693), null, 1L, null, false, false, false, new DateTime(2021, 10, 31, 14, 49, 14, 594, DateTimeKind.Local).AddTicks(6702), "123456778" });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyWallets_CustomerId",
                table: "MoneyWallets",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_CustomerId",
                table: "WalletTransactions",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointWallets");

            migrationBuilder.DropTable(
                name: "WalletTransactions");

            migrationBuilder.DropTable(
                name: "MoneyWallets");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
