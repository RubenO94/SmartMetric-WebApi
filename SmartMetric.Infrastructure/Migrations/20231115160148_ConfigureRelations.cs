using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingOptionTranslations_RatingOptions_RatingOptionId",
                table: "RatingOptionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceOptions_Questions_QuestionId",
                table: "SingleChoiceOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceOptionTranslations_SingleChoiceOptions_SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingOptionTranslations_RatingOptions_RatingOptionId",
                table: "RatingOptionTranslations",
                column: "RatingOptionId",
                principalTable: "RatingOptions",
                principalColumn: "RatingOptionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceOptions_Questions_QuestionId",
                table: "SingleChoiceOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceOptionTranslations_SingleChoiceOptions_SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations",
                column: "SingleChoiceOptionId",
                principalTable: "SingleChoiceOptions",
                principalColumn: "SingleChoiceOptionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingOptionTranslations_RatingOptions_RatingOptionId",
                table: "RatingOptionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceOptions_Questions_QuestionId",
                table: "SingleChoiceOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceOptionTranslations_SingleChoiceOptions_SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingOptionTranslations_RatingOptions_RatingOptionId",
                table: "RatingOptionTranslations",
                column: "RatingOptionId",
                principalTable: "RatingOptions",
                principalColumn: "RatingOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceOptions_Questions_QuestionId",
                table: "SingleChoiceOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceOptionTranslations_SingleChoiceOptions_SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations",
                column: "SingleChoiceOptionId",
                principalTable: "SingleChoiceOptions",
                principalColumn: "SingleChoiceOptionId");
        }
    }
}
