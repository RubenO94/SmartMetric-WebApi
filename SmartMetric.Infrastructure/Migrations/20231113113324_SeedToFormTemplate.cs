using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedToFormTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTranslations_Questions_QuestionId",
                table: "QuestionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingOptions_Questions_QuestionId",
                table: "RatingOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingOptionTranslations_RatingOptions_RatingOptionId",
                table: "RatingOptionTranslations");

            migrationBuilder.DeleteData(
                table: "FormTemplateTranslations",
                keyColumn: "FormTemplateTranslationId",
                keyValue: new Guid("0c5a48d3-e745-4e2b-b6e9-c93ec313072f"));

            migrationBuilder.DeleteData(
                table: "FormTemplateTranslations",
                keyColumn: "FormTemplateTranslationId",
                keyValue: new Guid("5e46bb2d-7801-4c38-9b5d-2e678f0b60c6"));

            migrationBuilder.DeleteData(
                table: "FormTemplateTranslations",
                keyColumn: "FormTemplateTranslationId",
                keyValue: new Guid("67404860-251f-4c6e-ba5c-2a848b2e7f85"));

            migrationBuilder.DeleteData(
                table: "FormTemplateTranslations",
                keyColumn: "FormTemplateTranslationId",
                keyValue: new Guid("b9e19ca5-df90-46b3-94ed-f5457a9f3e27"));

            migrationBuilder.DeleteData(
                table: "FormTemplates",
                keyColumn: "FormTemplateId",
                keyValue: new Guid("8c57f6d6-2a90-4f84-b32c-5c57d86e5c20"));

            migrationBuilder.DeleteData(
                table: "FormTemplates",
                keyColumn: "FormTemplateId",
                keyValue: new Guid("e4ea5e10-9b3e-4a45-8b3f-7cf7417e556f"));

            migrationBuilder.AlterColumn<Guid>(
                name: "RatingOptionId",
                table: "RatingOptionTranslations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "RatingOptions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "NumericValue",
                table: "RatingOptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "QuestionTranslations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "FormTemplates",
                columns: new[] { "FormTemplateId", "CreatedByUserId", "CreatedDate", "ModifiedDate" },
                values: new object[] { new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), 1, new DateTime(2023, 11, 13, 10, 51, 27, 873, DateTimeKind.Local), new DateTime(2023, 11, 13, 10, 51, 27, 873, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "IsRequired", "Position", "ResponseType" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"), true, null, "Rating" },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb1"), true, null, "SingleChoice" }
                });

            migrationBuilder.InsertData(
                table: "FormTemplateTranslations",
                columns: new[] { "FormTemplateTranslationId", "Description", "FormTemplateId", "Language", "Title" },
                values: new object[] { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"), "A survey to measure employee satisfaction.", new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), "en", "Employee Satisfaction Survey" });

            migrationBuilder.InsertData(
                table: "QuestionTranslations",
                columns: new[] { "QuestionTranslationId", "Description", "Language", "QuestionId", "Title" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afaa"), "Please rate your overall satisfaction with your work on a scale of 1 to 10.", "en", new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"), "How satisfied are you with your work?" },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb3"), "Please select your rating for the cafeteria food.", "en", new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb1"), "How would you rate the cafeteria food?" }
                });

            migrationBuilder.InsertData(
                table: "RatingOptions",
                columns: new[] { "RatingOptionId", "NumericValue", "QuestionId" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afab"), 1, new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8") },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afad"), 5, new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8") },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afaf"), 10, new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8") }
                });

            migrationBuilder.InsertData(
                table: "SingleChoiceOptions",
                columns: new[] { "SingleChoiceOptionId", "QuestionId" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb4"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb1") },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb6"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb1") },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb8"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb1") }
                });

            migrationBuilder.InsertData(
                table: "RatingOptionTranslations",
                columns: new[] { "RatingOptionTranslationId", "Description", "Language", "RatingOptionId" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afac"), "Not Satisfied", "en", new Guid("3fa85f64-5717-4562-b3fc-2c963f66afab") },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afae"), "Neutral", "en", new Guid("3fa85f64-5717-4562-b3fc-2c963f66afad") },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb0"), "Very Satisfied", "en", new Guid("3fa85f64-5717-4562-b3fc-2c963f66afaf") }
                });

            migrationBuilder.InsertData(
                table: "SingleChoiceOptionTranslations",
                columns: new[] { "SingleChoiceOptionTranslationId", "Description", "Language", "SingleChoiceOptionId" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb5"), "Excellent", "en", new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb4") },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb7"), "Good", "en", new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb6") },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb9"), "Fair", "en", new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb8") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTranslations_Questions_QuestionId",
                table: "QuestionTranslations",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingOptions_Questions_QuestionId",
                table: "RatingOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingOptionTranslations_RatingOptions_RatingOptionId",
                table: "RatingOptionTranslations",
                column: "RatingOptionId",
                principalTable: "RatingOptions",
                principalColumn: "RatingOptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTranslations_Questions_QuestionId",
                table: "QuestionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingOptions_Questions_QuestionId",
                table: "RatingOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingOptionTranslations_RatingOptions_RatingOptionId",
                table: "RatingOptionTranslations");

            migrationBuilder.DeleteData(
                table: "FormTemplateTranslations",
                keyColumn: "FormTemplateTranslationId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"));

            migrationBuilder.DeleteData(
                table: "QuestionTranslations",
                keyColumn: "QuestionTranslationId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afaa"));

            migrationBuilder.DeleteData(
                table: "QuestionTranslations",
                keyColumn: "QuestionTranslationId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb3"));

            migrationBuilder.DeleteData(
                table: "RatingOptionTranslations",
                keyColumn: "RatingOptionTranslationId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afac"));

            migrationBuilder.DeleteData(
                table: "RatingOptionTranslations",
                keyColumn: "RatingOptionTranslationId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afae"));

            migrationBuilder.DeleteData(
                table: "RatingOptionTranslations",
                keyColumn: "RatingOptionTranslationId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb0"));

            migrationBuilder.DeleteData(
                table: "SingleChoiceOptionTranslations",
                keyColumn: "SingleChoiceOptionTranslationId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb5"));

            migrationBuilder.DeleteData(
                table: "SingleChoiceOptionTranslations",
                keyColumn: "SingleChoiceOptionTranslationId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb7"));

            migrationBuilder.DeleteData(
                table: "SingleChoiceOptionTranslations",
                keyColumn: "SingleChoiceOptionTranslationId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb9"));

            migrationBuilder.DeleteData(
                table: "FormTemplates",
                keyColumn: "FormTemplateId",
                keyValue: new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"));

            migrationBuilder.DeleteData(
                table: "RatingOptions",
                keyColumn: "RatingOptionId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afab"));

            migrationBuilder.DeleteData(
                table: "RatingOptions",
                keyColumn: "RatingOptionId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afad"));

            migrationBuilder.DeleteData(
                table: "RatingOptions",
                keyColumn: "RatingOptionId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afaf"));

            migrationBuilder.DeleteData(
                table: "SingleChoiceOptions",
                keyColumn: "SingleChoiceOptionId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb4"));

            migrationBuilder.DeleteData(
                table: "SingleChoiceOptions",
                keyColumn: "SingleChoiceOptionId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb6"));

            migrationBuilder.DeleteData(
                table: "SingleChoiceOptions",
                keyColumn: "SingleChoiceOptionId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb8"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb1"));

            migrationBuilder.AlterColumn<Guid>(
                name: "RatingOptionId",
                table: "RatingOptionTranslations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "RatingOptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NumericValue",
                table: "RatingOptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "QuestionTranslations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "FormTemplates",
                columns: new[] { "FormTemplateId", "CreatedByUserId", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("8c57f6d6-2a90-4f84-b32c-5c57d86e5c20"), 1, new DateTime(2023, 11, 6, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("e4ea5e10-9b3e-4a45-8b3f-7cf7417e556f"), 2, new DateTime(2023, 11, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "FormTemplateTranslations",
                columns: new[] { "FormTemplateTranslationId", "Description", "FormTemplateId", "Language", "Title" },
                values: new object[,]
                {
                    { new Guid("0c5a48d3-e745-4e2b-b6e9-c93ec313072f"), "Form Template Description 1 (English)", new Guid("8c57f6d6-2a90-4f84-b32c-5c57d86e5c20"), "EN", "Form Template Title 1 (English)" },
                    { new Guid("5e46bb2d-7801-4c38-9b5d-2e678f0b60c6"), "Descrição do Modelo de Formulário 1", new Guid("8c57f6d6-2a90-4f84-b32c-5c57d86e5c20"), "PT", "Título do Modelo de Formulário 1" },
                    { new Guid("67404860-251f-4c6e-ba5c-2a848b2e7f85"), "Form Template Description 2 (English)", new Guid("e4ea5e10-9b3e-4a45-8b3f-7cf7417e556f"), "EN", "Form Template Title 2 (English)" },
                    { new Guid("b9e19ca5-df90-46b3-94ed-f5457a9f3e27"), "Descrição do Modelo de Formulário 2", new Guid("e4ea5e10-9b3e-4a45-8b3f-7cf7417e556f"), "PT", "Título do Modelo de Formulário 2" }
                });

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
        }
    }
}
