using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureRelations3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingOptions_Questions_QuestionId",
                table: "RatingOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingOptions_Questions_QuestionId",
                table: "RatingOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingOptions_Questions_QuestionId",
                table: "RatingOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingOptions_Questions_QuestionId",
                table: "RatingOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId");
        }
    }
}
