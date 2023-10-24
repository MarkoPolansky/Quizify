using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizify.Api.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedColumnGamePinInTableQuizToUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GamePin",
                table: "Quizzes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_GamePin",
                table: "Quizzes",
                column: "GamePin",
                unique: true,
                filter: "[GamePin] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Quizzes_GamePin",
                table: "Quizzes");

            migrationBuilder.AlterColumn<string>(
                name: "GamePin",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
