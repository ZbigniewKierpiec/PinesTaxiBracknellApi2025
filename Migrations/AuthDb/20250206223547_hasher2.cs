using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinesExecutiveTravelApi.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class hasher2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74938a76-f26a-4fc5-bf0e-d0a99d84ac18",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c39c5d65-3f77-46f3-b813-1de915d02cb1", "326ab33f-50b9-4fe8-a2c7-f542fce3c68a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74938a76-f26a-4fc5-bf0e-d0a99d84ac18",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c00263af-621e-4542-9a1f-56e1e02361c4", "440242b4-861e-4973-9600-c822b8c20a18" });
        }
    }
}
