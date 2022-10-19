using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CEGES_DataAccess.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipements_Groupes_GroupeId",
                table: "Equipements");

            migrationBuilder.AlterColumn<int>(
                name: "GroupeId",
                table: "Equipements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "Equipements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipements_Groupes_GroupeId",
                table: "Equipements",
                column: "GroupeId",
                principalTable: "Groupes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipements_Groupes_GroupeId",
                table: "Equipements");

            migrationBuilder.DropColumn(
                name: "Nom",
                table: "Equipements");

            migrationBuilder.AlterColumn<int>(
                name: "GroupeId",
                table: "Equipements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipements_Groupes_GroupeId",
                table: "Equipements",
                column: "GroupeId",
                principalTable: "Groupes",
                principalColumn: "Id");
        }
    }
}
