using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CEGES_DataAccess.Migrations
{
    public partial class changedMesureBackToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Mesure",
                table: "EquipementRapport",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Rapports",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateDebut",
                value: new DateTime(2022, 10, 29, 16, 7, 32, 980, DateTimeKind.Local).AddTicks(5610));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Mesure",
                table: "EquipementRapport",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Rapports",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateDebut",
                value: new DateTime(2022, 10, 29, 16, 5, 10, 801, DateTimeKind.Local).AddTicks(5152));
        }
    }
}
