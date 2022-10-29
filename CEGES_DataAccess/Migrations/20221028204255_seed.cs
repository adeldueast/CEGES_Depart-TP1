using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CEGES_DataAccess.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Entreprises",
                columns: new[] { "Id", "Nom" },
                values: new object[] { 1, "Ubisoft" });

            migrationBuilder.InsertData(
                table: "Rapports",
                columns: new[] { "Id", "DateDebut", "DateFin" },
                values: new object[] { 1, new DateTime(2022, 10, 28, 16, 42, 55, 530, DateTimeKind.Local).AddTicks(5225), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Groupes",
                columns: new[] { "Id", "EntrepriseId", "Nom" },
                values: new object[] { 1, 1, "Groupe 1" });

            migrationBuilder.InsertData(
                table: "Groupes",
                columns: new[] { "Id", "EntrepriseId", "Nom" },
                values: new object[] { 2, 1, "Groupe 2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groupes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groupes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rapports",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Entreprises",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
