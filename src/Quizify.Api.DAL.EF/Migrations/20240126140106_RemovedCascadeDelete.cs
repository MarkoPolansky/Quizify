using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizify.Api.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_ActiveInQuizId",
                table: "Questions");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_ActiveInQuizId",
                table: "Questions",
                column: "ActiveInQuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_ActiveInQuizId",
                table: "Questions");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_ActiveInQuizId",
                table: "Questions",
                column: "ActiveInQuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");
        }
    }
}
