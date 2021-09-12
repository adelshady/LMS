using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class AddAnswerQuestionToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Answer",
                newName: "Answer1");

            migrationBuilder.AddColumn<string>(
                name: "BestAnswer",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Answer2",
                table: "Answer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Answer3",
                table: "Answer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Answer4",
                table: "Answer",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestAnswer",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Answer2",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "Answer3",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "Answer4",
                table: "Answer");

            migrationBuilder.RenameColumn(
                name: "Answer1",
                table: "Answer",
                newName: "Name");
        }
    }
}
