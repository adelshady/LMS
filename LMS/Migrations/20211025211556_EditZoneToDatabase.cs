using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class EditZoneToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zones_students_StudentId",
                table: "Zones");

            migrationBuilder.DropIndex(
                name: "IX_Zones_StudentId",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Zones");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Zones",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Zones");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Zones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Zones_StudentId",
                table: "Zones",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zones_students_StudentId",
                table: "Zones",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
