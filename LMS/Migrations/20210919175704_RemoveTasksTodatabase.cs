using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class RemoveTasksTodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_calendars_tasks_TasksId",
                table: "calendars");

            migrationBuilder.DropIndex(
                name: "IX_calendars_TasksId",
                table: "calendars");

            migrationBuilder.DropColumn(
                name: "TasksId",
                table: "calendars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TasksId",
                table: "calendars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_calendars_TasksId",
                table: "calendars",
                column: "TasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_calendars_tasks_TasksId",
                table: "calendars",
                column: "TasksId",
                principalTable: "tasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
