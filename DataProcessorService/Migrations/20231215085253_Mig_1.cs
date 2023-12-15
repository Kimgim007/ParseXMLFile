using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataProcessorService.Migrations
{
    public partial class Mig_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModuleCategoreIDModeleStates",
                columns: table => new
                {
                    ModuleCategoreIDModeleStateID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModuleCategoreID = table.Column<string>(type: "TEXT", nullable: false),
                    ModeleState = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleCategoreIDModeleStates", x => x.ModuleCategoreIDModeleStateID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleCategoreIDModeleStates_ModuleCategoreID",
                table: "ModuleCategoreIDModeleStates",
                column: "ModuleCategoreID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleCategoreIDModeleStates");
        }
    }
}
