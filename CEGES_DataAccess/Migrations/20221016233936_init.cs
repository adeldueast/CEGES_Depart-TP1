using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CEGES_DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entreprises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entreprises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rapports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rapports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groupes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntrepriseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groupes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groupes_Entreprises_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupeId = table.Column<int>(type: "int", nullable: true),
                    Quantite = table.Column<float>(type: "real", nullable: true),
                    UniteMesure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacteurConversion = table.Column<int>(type: "int", nullable: true),
                    IntensiteZero = table.Column<float>(type: "real", nullable: true),
                    IntensiteMax = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipements_Groupes_GroupeId",
                        column: x => x.GroupeId,
                        principalTable: "Groupes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EquipementRapport",
                columns: table => new
                {
                    EquipementsId = table.Column<int>(type: "int", nullable: false),
                    RapportsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipementRapport", x => new { x.EquipementsId, x.RapportsId });
                    table.ForeignKey(
                        name: "FK_EquipementRapport_Equipements_EquipementsId",
                        column: x => x.EquipementsId,
                        principalTable: "Equipements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipementRapport_Rapports_RapportsId",
                        column: x => x.RapportsId,
                        principalTable: "Rapports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipementRapport_RapportsId",
                table: "EquipementRapport",
                column: "RapportsId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipements_GroupeId",
                table: "Equipements",
                column: "GroupeId");

            migrationBuilder.CreateIndex(
                name: "IX_Groupes_EntrepriseId",
                table: "Groupes",
                column: "EntrepriseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipementRapport");

            migrationBuilder.DropTable(
                name: "Equipements");

            migrationBuilder.DropTable(
                name: "Rapports");

            migrationBuilder.DropTable(
                name: "Groupes");

            migrationBuilder.DropTable(
                name: "Entreprises");
        }
    }
}
