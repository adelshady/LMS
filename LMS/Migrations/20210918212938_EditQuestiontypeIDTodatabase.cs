using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class EditQuestiontypeIDTodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionType_QuestionId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "OptionName",
                table: "Answer",
                newName: "AnswerName");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "Questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "QuestionTypeId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionType_QuestionId",
                table: "Questions",
                column: "QuestionId",
                principalTable: "QuestionType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionType_QuestionId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionTypeId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "AnswerName",
                table: "Answer",
                newName: "OptionName");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionType_QuestionId",
                table: "Questions",
                column: "QuestionId",
                principalTable: "QuestionType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
