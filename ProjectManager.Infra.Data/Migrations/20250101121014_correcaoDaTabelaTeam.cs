using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Migrations
{
    /// <inheritdoc />
    public partial class correcaoDaTabelaTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Teams_TeamModelId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Tasks_TaskModelId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TaskModelId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Projects_TeamModelId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TaskModelId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamModelId",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "TaskModelTeamModel",
                columns: table => new
                {
                    TasksId = table.Column<int>(type: "integer", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModelTeamModel", x => new { x.TasksId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TaskModelTeamModel_Tasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskModelTeamModel_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskModelTeamModel_TeamId",
                table: "TaskModelTeamModel",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskModelTeamModel");

            migrationBuilder.AddColumn<int>(
                name: "TaskModelId",
                table: "Teams",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamModelId",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TaskModelId",
                table: "Teams",
                column: "TaskModelId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Tasks_TaskModelId",
                table: "Teams",
                column: "TaskModelId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
