using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class EditCourseToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_HoDs_UserId",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "FK_courses_teachers_UserId",
                table: "courses");

            migrationBuilder.DropIndex(
                name: "IX_courses_UserId",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "courses");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "body",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_courses_HoDsId",
                table: "courses",
                column: "HoDsId");

            migrationBuilder.CreateIndex(
                name: "IX_courses_StudentId",
                table: "courses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_courses_TeacherId",
                table: "courses",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_HoDs_HoDsId",
                table: "courses",
                column: "HoDsId",
                principalTable: "HoDs",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_courses_students_StudentId",
                table: "courses",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_courses_teachers_TeacherId",
                table: "courses",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_HoDs_HoDsId",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "FK_courses_students_StudentId",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "FK_courses_teachers_TeacherId",
                table: "courses");

            migrationBuilder.DropIndex(
                name: "IX_courses_HoDsId",
                table: "courses");

            migrationBuilder.DropIndex(
                name: "IX_courses_StudentId",
                table: "courses");

            migrationBuilder.DropIndex(
                name: "IX_courses_TeacherId",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "courses");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "courses",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "body",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_courses_UserId",
                table: "courses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_HoDs_UserId",
                table: "courses",
                column: "UserId",
                principalTable: "HoDs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_courses_teachers_UserId",
                table: "courses",
                column: "UserId",
                principalTable: "teachers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
