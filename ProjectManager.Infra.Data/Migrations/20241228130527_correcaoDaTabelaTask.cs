using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Migrations
{
    /// <inheritdoc />
    public partial class correcaoDaTabelaTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskModelUserModel");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Tasks",
                newName: "TeamId");

            migrationBuilder.AddColumn<int>(
                name: "TaskModelId",
                table: "Teams",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Tasks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TaskModelId",
                table: "Teams",
                column: "TaskModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserModelId",
                table: "Tasks",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserModelId",
                table: "Tasks",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Tasks_TaskModelId",
                table: "Teams",
                column: "TaskModelId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserModelId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Tasks_TaskModelId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TaskModelId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserModelId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskModelId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Tasks",
                newName: "UserId");

            migrationBuilder.CreateTable(
                name: "TaskModelUserModel",
                columns: table => new
                {
                    TasksId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModelUserModel", x => new { x.TasksId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TaskModelUserModel_Tasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskModelUserModel_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskModelUserModel_UserId",
                table: "TaskModelUserModel",
                column: "UserId");
        }
    }
}
