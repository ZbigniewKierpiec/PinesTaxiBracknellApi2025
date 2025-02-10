using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinesExecutiveTravelApi.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class therd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74938a76-f26a-4fc5-bf0e-d0a99d84ac18",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9f09ea39-b098-4485-9a70-5d5e701a94e5", "AQAAAAIAAYagAAAAEBDyGf2+lUUJhi0tz/FuUIZ8Cgaa9fPUCurHtB1EOWKhC8Vey7mbQX0bKC8dMR+weg==", "448bd916-1801-4dd3-8c2c-4d5f546d64a4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74938a76-f26a-4fc5-bf0e-d0a99d84ac18",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c39c5d65-3f77-46f3-b813-1de915d02cb1", "AQAAAAEAACcQAAAAENx7G5rT8A5/1hXfjxRq/K+iQ==", "326ab33f-50b9-4fe8-a2c7-f542fce3c68a" });
        }
    }
}
