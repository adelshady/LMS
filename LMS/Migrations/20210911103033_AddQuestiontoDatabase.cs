using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class AddQuestiontoDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizDetails_QuestionType_QuestionId",
                table: "QuizDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizDetails_Questions_QuestionId",
                table: "QuizDetails",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizDetails_Questions_QuestionId",
                table: "QuizDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizDetails_QuestionType_QuestionId",
                table: "QuizDetails",
                column: "QuestionId",
                principalTable: "QuestionType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
