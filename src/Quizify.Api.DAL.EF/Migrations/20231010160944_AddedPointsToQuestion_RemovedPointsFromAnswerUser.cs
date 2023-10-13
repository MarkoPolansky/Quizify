using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizify.Api.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddedPointsToQuestion_RemovedPointsFromAnswerUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "AnswerUsers");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "AnswerUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
