using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinesExecutiveTravelApi.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class SECEND : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74938a76-f26a-4fc5-bf0e-d0a99d84ac18",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1a6c1a3-6fb3-4ca2-944d-c6e0e3b74c95", "AQAAAAIAAYagAAAAEAF3W6GNL6FDSCyhhmky/i2i6detwZQkHPP7WkAspGTTewDDAytbNClnq/GpgPoUFQ==", "64b30f8d-9125-42ba-93f6-e70503f93fd5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74938a76-f26a-4fc5-bf0e-d0a99d84ac18",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18a14226-4fec-4ae2-8029-dcfcb1aa14df", "AQAAAAIAAYagAAAAEACJf45KR7rbgXz+dWFXUj4jUh3peidS6gF2bGTEelHrOP9VB0zYx3kpAhSYAW22zQ==", "597342a4-7220-48d3-8085-c5af7e2dbdf0" });
        }
    }
}
