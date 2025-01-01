using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Migrations
{
    /// <inheritdoc />
    public partial class correcaoRelacionamentoTabelaTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskModelTeamModel");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TeamId",
                table: "Tasks",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Teams_TeamId",
                table: "Tasks",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Teams_TeamId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TeamId",
                table: "Tasks");

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
    }
}
