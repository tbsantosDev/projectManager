using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Migrations
{
    /// <inheritdoc />
    public partial class CorreçãoDaTabelaTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamModelId",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TeamModelId",
                table: "Projects",
                column: "TeamModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Teams_TeamModelId",
                table: "Projects",
                column: "TeamModelId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Teams_TeamModelId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_TeamModelId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TeamModelId",
                table: "Projects");
        }
    }
}
