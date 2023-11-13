using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedToFormTemplateQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FormTemplateQuestions",
                columns: new[] { "FormTemplateQuestionId", "FormTemplateId", "QuestionId" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afbb"), new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8") },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afbc"), new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb1") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FormTemplateQuestions",
                keyColumn: "FormTemplateQuestionId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afbb"));

            migrationBuilder.DeleteData(
                table: "FormTemplateQuestions",
                keyColumn: "FormTemplateQuestionId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afbc"));
        }
    }
}
