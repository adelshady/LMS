using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class AddMessageThreadToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "messageThread",
                columns: table => new
                {
                    IDMessageInitial = table.Column<int>(type: "int", nullable: false),
                    IDMessageReply = table.Column<int>(type: "int", nullable: false),
                    DateInserted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messageThread", x => new { x.IDMessageInitial, x.IDMessageReply });
                    table.ForeignKey(
                        name: "FK_messageThread_message_IDMessageInitial",
                        column: x => x.IDMessageInitial,
                        principalTable: "message",
                        principalColumn: "IDMessage",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_messageThread_message_IDMessageReply",
                        column: x => x.IDMessageReply,
                        principalTable: "message",
                        principalColumn: "IDMessage",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_messageThread_IDMessageReply",
                table: "messageThread",
                column: "IDMessageReply");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messageThread");
        }
    }
}
