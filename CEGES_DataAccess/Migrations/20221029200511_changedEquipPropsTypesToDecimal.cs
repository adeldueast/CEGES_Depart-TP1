using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CEGES_DataAccess.Migrations
{
    public partial class changedEquipPropsTypesToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantite",
                table: "Equipements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "IntensiteZero",
                table: "Equipements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "IntensiteMax",
                table: "Equipements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FacteurConversion",
                table: "Equipements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Quantite",
                table: "Equipements",
                type: "real",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "IntensiteZero",
                table: "Equipements",
                type: "real",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "IntensiteMax",
                table: "Equipements",
                type: "real",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FacteurConversion",
                table: "Equipements",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

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
                value: new DateTime(2022, 10, 28, 16, 42, 55, 530, DateTimeKind.Local).AddTicks(5225));
        }
    }
}
