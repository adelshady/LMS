using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class EditTwoCourseToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_HoDs_HoDsId",
                table: "courses");

            migrationBuilder.DropIndex(
                name: "IX_courses_HoDsId",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "HoDsId",
                table: "courses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HoDsId",
                table: "courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_courses_HoDsId",
                table: "courses",
                column: "HoDsId");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_HoDs_HoDsId",
                table: "courses",
                column: "HoDsId",
                principalTable: "HoDs",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
