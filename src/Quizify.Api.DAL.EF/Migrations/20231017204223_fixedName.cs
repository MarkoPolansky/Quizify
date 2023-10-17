using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizify.Api.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class fixedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsStatred",
                table: "Quizzes",
                newName: "IsStarted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsStarted",
                table: "Quizzes",
                newName: "IsStatred");
        }
    }
}
