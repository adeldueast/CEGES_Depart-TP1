using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CEGES_DataAccess.Migrations
{
    public partial class addedMesure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipementRapport_Equipements_EquipementsId",
                table: "EquipementRapport");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipementRapport_Rapports_RapportsId",
                table: "EquipementRapport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipementRapport",
                table: "EquipementRapport");

            migrationBuilder.DropIndex(
                name: "IX_EquipementRapport_RapportsId",
                table: "EquipementRapport");

            migrationBuilder.RenameColumn(
                name: "RapportsId",
                table: "EquipementRapport",
                newName: "Mesure");

            migrationBuilder.RenameColumn(
                name: "EquipementsId",
                table: "EquipementRapport",
                newName: "RapportId");

            migrationBuilder.AddColumn<int>(
                name: "EquipementId",
                table: "EquipementRapport",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipementRapport",
                table: "EquipementRapport",
                columns: new[] { "EquipementId", "RapportId" });

            migrationBuilder.CreateIndex(
                name: "IX_EquipementRapport_RapportId",
                table: "EquipementRapport",
                column: "RapportId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipementRapport_Equipements_EquipementId",
                table: "EquipementRapport",
                column: "EquipementId",
                principalTable: "Equipements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipementRapport_Rapports_RapportId",
                table: "EquipementRapport",
                column: "RapportId",
                principalTable: "Rapports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipementRapport_Equipements_EquipementId",
                table: "EquipementRapport");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipementRapport_Rapports_RapportId",
                table: "EquipementRapport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipementRapport",
                table: "EquipementRapport");

            migrationBuilder.DropIndex(
                name: "IX_EquipementRapport_RapportId",
                table: "EquipementRapport");

            migrationBuilder.DropColumn(
                name: "EquipementId",
                table: "EquipementRapport");

            migrationBuilder.RenameColumn(
                name: "Mesure",
                table: "EquipementRapport",
                newName: "RapportsId");

            migrationBuilder.RenameColumn(
                name: "RapportId",
                table: "EquipementRapport",
                newName: "EquipementsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipementRapport",
                table: "EquipementRapport",
                columns: new[] { "EquipementsId", "RapportsId" });

            migrationBuilder.CreateIndex(
                name: "IX_EquipementRapport_RapportsId",
                table: "EquipementRapport",
                column: "RapportsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipementRapport_Equipements_EquipementsId",
                table: "EquipementRapport",
                column: "EquipementsId",
                principalTable: "Equipements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipementRapport_Rapports_RapportsId",
                table: "EquipementRapport",
                column: "RapportsId",
                principalTable: "Rapports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
