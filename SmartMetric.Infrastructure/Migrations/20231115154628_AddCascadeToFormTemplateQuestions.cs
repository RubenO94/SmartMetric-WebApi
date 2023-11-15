using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeToFormTemplateQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTranslations_Questions_QuestionId",
                table: "QuestionTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormTemplateQuestions",
                table: "FormTemplateQuestions");

            migrationBuilder.DropIndex(
                name: "IX_FormTemplateQuestions_FormTemplateId",
                table: "FormTemplateQuestions");

            migrationBuilder.DeleteData(
                table: "FormTemplateQuestions",
                keyColumn: "FormTemplateQuestionId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afbb"));

            migrationBuilder.DeleteData(
                table: "FormTemplateQuestions",
                keyColumn: "FormTemplateQuestionId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afbc"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormTemplateQuestions",
                table: "FormTemplateQuestions",
                columns: new[] { "FormTemplateId", "QuestionId" });

            migrationBuilder.InsertData(
                table: "FormTemplateQuestions",
                columns: new[] { "FormTemplateId", "QuestionId", "FormTemplateQuestionId" },
                values: new object[,]
                {
                    { new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afbb") },
                    { new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb1"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afbc") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTranslations_Questions_QuestionId",
                table: "QuestionTranslations",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTranslations_Questions_QuestionId",
                table: "QuestionTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormTemplateQuestions",
                table: "FormTemplateQuestions");

            migrationBuilder.DeleteData(
                table: "FormTemplateQuestions",
                keyColumns: new[] { "FormTemplateId", "QuestionId" },
                keyValues: new object[] { new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8") });

            migrationBuilder.DeleteData(
                table: "FormTemplateQuestions",
                keyColumns: new[] { "FormTemplateId", "QuestionId" },
                keyValues: new object[] { new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb1") });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormTemplateQuestions",
                table: "FormTemplateQuestions",
                column: "FormTemplateQuestionId");

            migrationBuilder.InsertData(
                table: "FormTemplateQuestions",
                columns: new[] { "FormTemplateQuestionId", "FormTemplateId", "QuestionId" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afbb"), new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8") },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afbc"), new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb1") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplateQuestions_FormTemplateId",
                table: "FormTemplateQuestions",
                column: "FormTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTranslations_Questions_QuestionId",
                table: "QuestionTranslations",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId");
        }
    }
}
