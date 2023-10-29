using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizify.Api.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnActiveInQuizInTableQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_Id",
                table: "Questions");

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveInQuizId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ActiveInQuizId",
                table: "Questions",
                column: "ActiveInQuizId",
                unique: true,
                filter: "[ActiveInQuizId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_ActiveInQuizId",
                table: "Questions",
                column: "ActiveInQuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_ActiveInQuizId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ActiveInQuizId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ActiveInQuizId",
                table: "Questions");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_Id",
                table: "Questions",
                column: "Id",
                principalTable: "Quizzes",
                principalColumn: "Id");
        }
    }
}
