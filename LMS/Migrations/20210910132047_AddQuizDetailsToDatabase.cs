using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class AddQuizDetailsToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    receivedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Attempts = table.Column<int>(type: "int", nullable: false),
                    ChapterId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuizDetails_chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "chapters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizDetails_QuestionType_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizDetails_ChapterId",
                table: "QuizDetails",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizDetails_QuestionId",
                table: "QuizDetails",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizDetails");
        }
    }
}
