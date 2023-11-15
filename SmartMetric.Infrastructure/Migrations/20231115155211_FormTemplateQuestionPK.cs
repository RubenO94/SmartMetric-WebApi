using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FormTemplateQuestionPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormTemplateQuestionId",
                table: "FormTemplateQuestions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FormTemplateQuestionId",
                table: "FormTemplateQuestions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "FormTemplateQuestions",
                keyColumns: new[] { "FormTemplateId", "QuestionId" },
                keyValues: new object[] { new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8") },
                column: "FormTemplateQuestionId",
                value: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afbb"));

            migrationBuilder.UpdateData(
                table: "FormTemplateQuestions",
                keyColumns: new[] { "FormTemplateId", "QuestionId" },
                keyValues: new object[] { new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb1") },
                column: "FormTemplateQuestionId",
                value: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afbc"));
        }
    }
}
