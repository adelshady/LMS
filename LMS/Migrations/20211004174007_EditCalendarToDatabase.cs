using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class EditCalendarToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_users_UserId",
                table: "courses");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "HoDsId",
                table: "courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "day",
                table: "calendars",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_HoDs_UserId",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "FK_courses_teachers_UserId",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "HoDsId",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "day",
                table: "calendars");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_courses_users_UserId",
                table: "courses",
                column: "UserId",
                principalTable: "users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
