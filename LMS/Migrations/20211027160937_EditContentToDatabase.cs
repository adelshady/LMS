using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Migrations
{
    public partial class EditContentToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file",
                table: "Contents");

            migrationBuilder.AddColumn<byte[]>(
                name: "VideoData",
                table: "Contents",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoData",
                table: "Contents");

            migrationBuilder.AddColumn<string>(
                name: "file",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
