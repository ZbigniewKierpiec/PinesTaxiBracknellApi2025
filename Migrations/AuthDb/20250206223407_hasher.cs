using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinesExecutiveTravelApi.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class hasher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74938a76-f26a-4fc5-bf0e-d0a99d84ac18",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c00263af-621e-4542-9a1f-56e1e02361c4", "AQAAAAEAACcQAAAAENx7G5rT8A5/1hXfjxRq/K+iQ==", "440242b4-861e-4973-9600-c822b8c20a18" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74938a76-f26a-4fc5-bf0e-d0a99d84ac18",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1a6c1a3-6fb3-4ca2-944d-c6e0e3b74c95", "AQAAAAIAAYagAAAAEAF3W6GNL6FDSCyhhmky/i2i6detwZQkHPP7WkAspGTTewDDAytbNClnq/GpgPoUFQ==", "64b30f8d-9125-42ba-93f6-e70503f93fd5" });
        }
    }
}
