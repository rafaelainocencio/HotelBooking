using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class CorrectColumnNameInMaintenace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InMaintence",
                table: "Rooms",
                newName: "InMaintenance");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price_Value",
                table: "Rooms",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InMaintenance",
                table: "Rooms",
                newName: "InMaintence");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price_Value",
                table: "Rooms",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");
        }
    }
}
