using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizify.Api.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class DeletedDuplicateForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerUsers_Answers_AnswerEntityId",
                table: "AnswerUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerUsers_Users_UserEntityId",
                table: "AnswerUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizUsers_Quizzes_QuizEntityId",
                table: "QuizUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizUsers_Users_UserEntityId",
                table: "QuizUsers");

            migrationBuilder.DropIndex(
                name: "IX_QuizUsers_QuizEntityId",
                table: "QuizUsers");

            migrationBuilder.DropIndex(
                name: "IX_QuizUsers_UserEntityId",
                table: "QuizUsers");

            migrationBuilder.DropIndex(
                name: "IX_AnswerUsers_AnswerEntityId",
                table: "AnswerUsers");

            migrationBuilder.DropIndex(
                name: "IX_AnswerUsers_UserEntityId",
                table: "AnswerUsers");

            migrationBuilder.DropColumn(
                name: "QuizEntityId",
                table: "QuizUsers");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "QuizUsers");

            migrationBuilder.DropColumn(
                name: "AnswerEntityId",
                table: "AnswerUsers");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "AnswerUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QuizEntityId",
                table: "QuizUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "QuizUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AnswerEntityId",
                table: "AnswerUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "AnswerUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizUsers_QuizEntityId",
                table: "QuizUsers",
                column: "QuizEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizUsers_UserEntityId",
                table: "QuizUsers",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerUsers_AnswerEntityId",
                table: "AnswerUsers",
                column: "AnswerEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerUsers_UserEntityId",
                table: "AnswerUsers",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerUsers_Answers_AnswerEntityId",
                table: "AnswerUsers",
                column: "AnswerEntityId",
                principalTable: "Answers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerUsers_Users_UserEntityId",
                table: "AnswerUsers",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizUsers_Quizzes_QuizEntityId",
                table: "QuizUsers",
                column: "QuizEntityId",
                principalTable: "Quizzes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizUsers_Users_UserEntityId",
                table: "QuizUsers",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
