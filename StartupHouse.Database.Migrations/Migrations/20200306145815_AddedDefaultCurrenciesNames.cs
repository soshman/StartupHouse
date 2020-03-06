using Microsoft.EntityFrameworkCore.Migrations;

namespace StartupHouse.Database.Migrations.Migrations
{
    public partial class AddedDefaultCurrenciesNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "Name",
                value: "Euro");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "Name",
                value: "Dolar amerykański");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "Name",
                value: null);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "Name",
                value: null);
        }
    }
}
