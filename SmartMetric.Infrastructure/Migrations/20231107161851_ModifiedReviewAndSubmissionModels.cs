using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedReviewAndSubmissionModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "EvaluatorEmployeeId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "EvaluatedEmployeeId",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EvaluatorEmployeeId",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "SingleChoiceTemplateTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "SingleChoiceOptionTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "RatingTemplateTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "RatingOptionTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "QuestionTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "FormTemplateTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "FormTemplateTranslations",
                keyColumn: "FormTemplateTranslationId",
                keyValue: new Guid("0c5a48d3-e745-4e2b-b6e9-c93ec313072f"),
                column: "Language",
                value: "EN");

            migrationBuilder.UpdateData(
                table: "FormTemplateTranslations",
                keyColumn: "FormTemplateTranslationId",
                keyValue: new Guid("5e46bb2d-7801-4c38-9b5d-2e678f0b60c6"),
                column: "Language",
                value: "PT");

            migrationBuilder.UpdateData(
                table: "FormTemplateTranslations",
                keyColumn: "FormTemplateTranslationId",
                keyValue: new Guid("67404860-251f-4c6e-ba5c-2a848b2e7f85"),
                column: "Language",
                value: "EN");

            migrationBuilder.UpdateData(
                table: "FormTemplateTranslations",
                keyColumn: "FormTemplateTranslationId",
                keyValue: new Guid("b9e19ca5-df90-46b3-94ed-f5457a9f3e27"),
                column: "Language",
                value: "PT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvaluatedEmployeeId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "EvaluatorEmployeeId",
                table: "Submissions");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "Language",
                table: "SingleChoiceTemplateTranslations",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Language",
                table: "SingleChoiceOptionTranslations",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvaluatorEmployeeId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Language",
                table: "RatingTemplateTranslations",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Language",
                table: "RatingOptionTranslations",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Language",
                table: "QuestionTranslations",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Language",
                table: "FormTemplateTranslations",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "FormTemplateTranslations",
                keyColumn: "FormTemplateTranslationId",
                keyValue: new Guid("0c5a48d3-e745-4e2b-b6e9-c93ec313072f"),
                column: "Language",
                value: 1);

            migrationBuilder.UpdateData(
                table: "FormTemplateTranslations",
                keyColumn: "FormTemplateTranslationId",
                keyValue: new Guid("5e46bb2d-7801-4c38-9b5d-2e678f0b60c6"),
                column: "Language",
                value: 2);

            migrationBuilder.UpdateData(
                table: "FormTemplateTranslations",
                keyColumn: "FormTemplateTranslationId",
                keyValue: new Guid("67404860-251f-4c6e-ba5c-2a848b2e7f85"),
                column: "Language",
                value: 1);

            migrationBuilder.UpdateData(
                table: "FormTemplateTranslations",
                keyColumn: "FormTemplateTranslationId",
                keyValue: new Guid("b9e19ca5-df90-46b3-94ed-f5457a9f3e27"),
                column: "Language",
                value: 2);
        }
    }
}
