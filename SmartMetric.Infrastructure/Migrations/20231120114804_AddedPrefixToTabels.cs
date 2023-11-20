using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPrefixToTabels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplateTranslations_FormTemplates_FormTemplateId",
                table: "FormTemplateTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_FormTemplates_FormTemplateId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Reviews_ReviewId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTranslations_Questions_QuestionId",
                table: "QuestionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingOptions_Questions_QuestionId",
                table: "RatingOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingOptionTranslations_RatingOptions_RatingOptionId",
                table: "RatingOptionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewResponses_Questions_QuestionId",
                table: "ReviewResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewResponses_SingleChoiceOptions_SingleChoiceOptionId",
                table: "ReviewResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewResponses_Submissions_SubmissionId",
                table: "ReviewResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceOptions_Questions_QuestionId",
                table: "SingleChoiceOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceOptionTranslations_SingleChoiceOptions_SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Reviews_ReviewId",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SingleChoiceOptionTranslations",
                table: "SingleChoiceOptionTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SingleChoiceOptions",
                table: "SingleChoiceOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewResponses",
                table: "ReviewResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RatingOptionTranslations",
                table: "RatingOptionTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RatingOptions",
                table: "RatingOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionTranslations",
                table: "QuestionTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormTemplateTranslations",
                table: "FormTemplateTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormTemplates",
                table: "FormTemplates");

            migrationBuilder.RenameTable(
                name: "Submissions",
                newName: "Metrics_Submissions");

            migrationBuilder.RenameTable(
                name: "SingleChoiceOptionTranslations",
                newName: "Metrics_SingleChoiceOptionTranslations");

            migrationBuilder.RenameTable(
                name: "SingleChoiceOptions",
                newName: "Metrics_SingleChoiceOptions");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Metrics_Reviews");

            migrationBuilder.RenameTable(
                name: "ReviewResponses",
                newName: "Metrics_ReviewResponses");

            migrationBuilder.RenameTable(
                name: "RatingOptionTranslations",
                newName: "Metrics_RatingOptionTranslations");

            migrationBuilder.RenameTable(
                name: "RatingOptions",
                newName: "Metrics_RatingOptions");

            migrationBuilder.RenameTable(
                name: "QuestionTranslations",
                newName: "Metrics_QuestionTranslations");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Metrics_Questions");

            migrationBuilder.RenameTable(
                name: "FormTemplateTranslations",
                newName: "Metrics_FormTemplateTranslations");

            migrationBuilder.RenameTable(
                name: "FormTemplates",
                newName: "Metrics_FormTemplates");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_ReviewId",
                table: "Metrics_Submissions",
                newName: "IX_Metrics_Submissions_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_SingleChoiceOptionTranslations_SingleChoiceOptionId",
                table: "Metrics_SingleChoiceOptionTranslations",
                newName: "IX_Metrics_SingleChoiceOptionTranslations_SingleChoiceOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_SingleChoiceOptions_QuestionId",
                table: "Metrics_SingleChoiceOptions",
                newName: "IX_Metrics_SingleChoiceOptions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewResponses_SubmissionId",
                table: "Metrics_ReviewResponses",
                newName: "IX_Metrics_ReviewResponses_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewResponses_SingleChoiceOptionId",
                table: "Metrics_ReviewResponses",
                newName: "IX_Metrics_ReviewResponses_SingleChoiceOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewResponses_QuestionId",
                table: "Metrics_ReviewResponses",
                newName: "IX_Metrics_ReviewResponses_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_RatingOptionTranslations_RatingOptionId",
                table: "Metrics_RatingOptionTranslations",
                newName: "IX_Metrics_RatingOptionTranslations_RatingOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_RatingOptions_QuestionId",
                table: "Metrics_RatingOptions",
                newName: "IX_Metrics_RatingOptions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionTranslations_QuestionId",
                table: "Metrics_QuestionTranslations",
                newName: "IX_Metrics_QuestionTranslations_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_ReviewId",
                table: "Metrics_Questions",
                newName: "IX_Metrics_Questions_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_FormTemplateId",
                table: "Metrics_Questions",
                newName: "IX_Metrics_Questions_FormTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_FormTemplateTranslations_FormTemplateId",
                table: "Metrics_FormTemplateTranslations",
                newName: "IX_Metrics_FormTemplateTranslations_FormTemplateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metrics_Submissions",
                table: "Metrics_Submissions",
                column: "SubmissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metrics_SingleChoiceOptionTranslations",
                table: "Metrics_SingleChoiceOptionTranslations",
                column: "SingleChoiceOptionTranslationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metrics_SingleChoiceOptions",
                table: "Metrics_SingleChoiceOptions",
                column: "SingleChoiceOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metrics_Reviews",
                table: "Metrics_Reviews",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metrics_ReviewResponses",
                table: "Metrics_ReviewResponses",
                column: "ReviewResponseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metrics_RatingOptionTranslations",
                table: "Metrics_RatingOptionTranslations",
                column: "RatingOptionTranslationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metrics_RatingOptions",
                table: "Metrics_RatingOptions",
                column: "RatingOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metrics_QuestionTranslations",
                table: "Metrics_QuestionTranslations",
                column: "QuestionTranslationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metrics_Questions",
                table: "Metrics_Questions",
                column: "QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metrics_FormTemplateTranslations",
                table: "Metrics_FormTemplateTranslations",
                column: "FormTemplateTranslationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metrics_FormTemplates",
                table: "Metrics_FormTemplates",
                column: "FormTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_FormTemplateTranslations_Metrics_FormTemplates_FormTemplateId",
                table: "Metrics_FormTemplateTranslations",
                column: "FormTemplateId",
                principalTable: "Metrics_FormTemplates",
                principalColumn: "FormTemplateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_Questions_Metrics_FormTemplates_FormTemplateId",
                table: "Metrics_Questions",
                column: "FormTemplateId",
                principalTable: "Metrics_FormTemplates",
                principalColumn: "FormTemplateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_Questions_Metrics_Reviews_ReviewId",
                table: "Metrics_Questions",
                column: "ReviewId",
                principalTable: "Metrics_Reviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_QuestionTranslations_Metrics_Questions_QuestionId",
                table: "Metrics_QuestionTranslations",
                column: "QuestionId",
                principalTable: "Metrics_Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_RatingOptions_Metrics_Questions_QuestionId",
                table: "Metrics_RatingOptions",
                column: "QuestionId",
                principalTable: "Metrics_Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_RatingOptionTranslations_Metrics_RatingOptions_RatingOptionId",
                table: "Metrics_RatingOptionTranslations",
                column: "RatingOptionId",
                principalTable: "Metrics_RatingOptions",
                principalColumn: "RatingOptionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_ReviewResponses_Metrics_Questions_QuestionId",
                table: "Metrics_ReviewResponses",
                column: "QuestionId",
                principalTable: "Metrics_Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_ReviewResponses_Metrics_SingleChoiceOptions_SingleChoiceOptionId",
                table: "Metrics_ReviewResponses",
                column: "SingleChoiceOptionId",
                principalTable: "Metrics_SingleChoiceOptions",
                principalColumn: "SingleChoiceOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_ReviewResponses_Metrics_Submissions_SubmissionId",
                table: "Metrics_ReviewResponses",
                column: "SubmissionId",
                principalTable: "Metrics_Submissions",
                principalColumn: "SubmissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_SingleChoiceOptions_Metrics_Questions_QuestionId",
                table: "Metrics_SingleChoiceOptions",
                column: "QuestionId",
                principalTable: "Metrics_Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_SingleChoiceOptionTranslations_Metrics_SingleChoiceOptions_SingleChoiceOptionId",
                table: "Metrics_SingleChoiceOptionTranslations",
                column: "SingleChoiceOptionId",
                principalTable: "Metrics_SingleChoiceOptions",
                principalColumn: "SingleChoiceOptionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_Submissions_Metrics_Reviews_ReviewId",
                table: "Metrics_Submissions",
                column: "ReviewId",
                principalTable: "Metrics_Reviews",
                principalColumn: "ReviewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_FormTemplateTranslations_Metrics_FormTemplates_FormTemplateId",
                table: "Metrics_FormTemplateTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Questions_Metrics_FormTemplates_FormTemplateId",
                table: "Metrics_Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Questions_Metrics_Reviews_ReviewId",
                table: "Metrics_Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_QuestionTranslations_Metrics_Questions_QuestionId",
                table: "Metrics_QuestionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_RatingOptions_Metrics_Questions_QuestionId",
                table: "Metrics_RatingOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_RatingOptionTranslations_Metrics_RatingOptions_RatingOptionId",
                table: "Metrics_RatingOptionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_ReviewResponses_Metrics_Questions_QuestionId",
                table: "Metrics_ReviewResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_ReviewResponses_Metrics_SingleChoiceOptions_SingleChoiceOptionId",
                table: "Metrics_ReviewResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_ReviewResponses_Metrics_Submissions_SubmissionId",
                table: "Metrics_ReviewResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_SingleChoiceOptions_Metrics_Questions_QuestionId",
                table: "Metrics_SingleChoiceOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_SingleChoiceOptionTranslations_Metrics_SingleChoiceOptions_SingleChoiceOptionId",
                table: "Metrics_SingleChoiceOptionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Submissions_Metrics_Reviews_ReviewId",
                table: "Metrics_Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metrics_Submissions",
                table: "Metrics_Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metrics_SingleChoiceOptionTranslations",
                table: "Metrics_SingleChoiceOptionTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metrics_SingleChoiceOptions",
                table: "Metrics_SingleChoiceOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metrics_Reviews",
                table: "Metrics_Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metrics_ReviewResponses",
                table: "Metrics_ReviewResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metrics_RatingOptionTranslations",
                table: "Metrics_RatingOptionTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metrics_RatingOptions",
                table: "Metrics_RatingOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metrics_QuestionTranslations",
                table: "Metrics_QuestionTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metrics_Questions",
                table: "Metrics_Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metrics_FormTemplateTranslations",
                table: "Metrics_FormTemplateTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metrics_FormTemplates",
                table: "Metrics_FormTemplates");

            migrationBuilder.RenameTable(
                name: "Metrics_Submissions",
                newName: "Submissions");

            migrationBuilder.RenameTable(
                name: "Metrics_SingleChoiceOptionTranslations",
                newName: "SingleChoiceOptionTranslations");

            migrationBuilder.RenameTable(
                name: "Metrics_SingleChoiceOptions",
                newName: "SingleChoiceOptions");

            migrationBuilder.RenameTable(
                name: "Metrics_Reviews",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Metrics_ReviewResponses",
                newName: "ReviewResponses");

            migrationBuilder.RenameTable(
                name: "Metrics_RatingOptionTranslations",
                newName: "RatingOptionTranslations");

            migrationBuilder.RenameTable(
                name: "Metrics_RatingOptions",
                newName: "RatingOptions");

            migrationBuilder.RenameTable(
                name: "Metrics_QuestionTranslations",
                newName: "QuestionTranslations");

            migrationBuilder.RenameTable(
                name: "Metrics_Questions",
                newName: "Questions");

            migrationBuilder.RenameTable(
                name: "Metrics_FormTemplateTranslations",
                newName: "FormTemplateTranslations");

            migrationBuilder.RenameTable(
                name: "Metrics_FormTemplates",
                newName: "FormTemplates");

            migrationBuilder.RenameIndex(
                name: "IX_Metrics_Submissions_ReviewId",
                table: "Submissions",
                newName: "IX_Submissions_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Metrics_SingleChoiceOptionTranslations_SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations",
                newName: "IX_SingleChoiceOptionTranslations_SingleChoiceOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Metrics_SingleChoiceOptions_QuestionId",
                table: "SingleChoiceOptions",
                newName: "IX_SingleChoiceOptions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Metrics_ReviewResponses_SubmissionId",
                table: "ReviewResponses",
                newName: "IX_ReviewResponses_SubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_Metrics_ReviewResponses_SingleChoiceOptionId",
                table: "ReviewResponses",
                newName: "IX_ReviewResponses_SingleChoiceOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Metrics_ReviewResponses_QuestionId",
                table: "ReviewResponses",
                newName: "IX_ReviewResponses_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Metrics_RatingOptionTranslations_RatingOptionId",
                table: "RatingOptionTranslations",
                newName: "IX_RatingOptionTranslations_RatingOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Metrics_RatingOptions_QuestionId",
                table: "RatingOptions",
                newName: "IX_RatingOptions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Metrics_QuestionTranslations_QuestionId",
                table: "QuestionTranslations",
                newName: "IX_QuestionTranslations_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Metrics_Questions_ReviewId",
                table: "Questions",
                newName: "IX_Questions_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Metrics_Questions_FormTemplateId",
                table: "Questions",
                newName: "IX_Questions_FormTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_Metrics_FormTemplateTranslations_FormTemplateId",
                table: "FormTemplateTranslations",
                newName: "IX_FormTemplateTranslations_FormTemplateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "SubmissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SingleChoiceOptionTranslations",
                table: "SingleChoiceOptionTranslations",
                column: "SingleChoiceOptionTranslationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SingleChoiceOptions",
                table: "SingleChoiceOptions",
                column: "SingleChoiceOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewResponses",
                table: "ReviewResponses",
                column: "ReviewResponseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RatingOptionTranslations",
                table: "RatingOptionTranslations",
                column: "RatingOptionTranslationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RatingOptions",
                table: "RatingOptions",
                column: "RatingOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionTranslations",
                table: "QuestionTranslations",
                column: "QuestionTranslationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormTemplateTranslations",
                table: "FormTemplateTranslations",
                column: "FormTemplateTranslationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormTemplates",
                table: "FormTemplates",
                column: "FormTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplateTranslations_FormTemplates_FormTemplateId",
                table: "FormTemplateTranslations",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "FormTemplateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_FormTemplates_FormTemplateId",
                table: "Questions",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "FormTemplateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Reviews_ReviewId",
                table: "Questions",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTranslations_Questions_QuestionId",
                table: "QuestionTranslations",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingOptions_Questions_QuestionId",
                table: "RatingOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingOptionTranslations_RatingOptions_RatingOptionId",
                table: "RatingOptionTranslations",
                column: "RatingOptionId",
                principalTable: "RatingOptions",
                principalColumn: "RatingOptionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewResponses_Questions_QuestionId",
                table: "ReviewResponses",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewResponses_SingleChoiceOptions_SingleChoiceOptionId",
                table: "ReviewResponses",
                column: "SingleChoiceOptionId",
                principalTable: "SingleChoiceOptions",
                principalColumn: "SingleChoiceOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewResponses_Submissions_SubmissionId",
                table: "ReviewResponses",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "SubmissionId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Reviews_ReviewId",
                table: "Submissions",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId");
        }
    }
}
