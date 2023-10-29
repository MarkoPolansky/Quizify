using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizify.Api.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class EditedQuestionEntityAndQuizEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStarted",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "TimeLimit",
                table: "Quizzes");

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveQuestionId",
                table: "Quizzes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuizState",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeLimit",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_Id",
                table: "Questions",
                column: "Id",
                principalTable: "Quizzes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_Id",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ActiveQuestionId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuizState",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TimeLimit",
                table: "Questions");

            migrationBuilder.AddColumn<bool>(
                name: "IsStarted",
                table: "Quizzes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeLimit",
                table: "Quizzes",
                type: "time",
                nullable: true);
        }
    }
}
