using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedToFormTemplatesTranslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FormTemplateTranslations",
                columns: new[] { "FormTemplateTranslationId", "Description", "FormTemplateId", "Language", "Title" },
                values: new object[,]
                {
                    { new Guid("0c5a48d3-e745-4e2b-b6e9-c93ec313072f"), "Form Template Description 1 (English)", new Guid("8c57f6d6-2a90-4f84-b32c-5c57d86e5c20"), 1, "Form Template Title 1 (English)" },
                    { new Guid("5e46bb2d-7801-4c38-9b5d-2e678f0b60c6"), "Descrição do Modelo de Formulário 1", new Guid("8c57f6d6-2a90-4f84-b32c-5c57d86e5c20"), 2, "Título do Modelo de Formulário 1" },
                    { new Guid("67404860-251f-4c6e-ba5c-2a848b2e7f85"), "Form Template Description 2 (English)", new Guid("e4ea5e10-9b3e-4a45-8b3f-7cf7417e556f"), 1, "Form Template Title 2 (English)" },
                    { new Guid("b9e19ca5-df90-46b3-94ed-f5457a9f3e27"), "Descrição do Modelo de Formulário 2", new Guid("e4ea5e10-9b3e-4a45-8b3f-7cf7417e556f"), 2, "Título do Modelo de Formulário 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
