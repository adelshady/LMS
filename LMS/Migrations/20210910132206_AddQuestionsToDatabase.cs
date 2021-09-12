using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class AddQuestionsToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Degree = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Questions_Answer_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionType_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AnswerId",
                table: "Questions",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionId",
                table: "Questions",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
