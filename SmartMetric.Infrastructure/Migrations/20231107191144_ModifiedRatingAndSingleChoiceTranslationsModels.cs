using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedRatingAndSingleChoiceTranslationsModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingOptionTranslations_RatingOptions_RatingOptionId",
                table: "RatingOptionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingOptionTranslations_RatingTemplates_RatingTemplateId",
                table: "RatingOptionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceOptionTranslations_SingleChoiceOptions_SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceOptionTranslations_SingleChoiceTemplates_SingleChoiceTemplateId",
                table: "SingleChoiceOptionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_SingleChoiceOptionTranslations_SingleChoiceTemplateId",
                table: "SingleChoiceOptionTranslations");

            migrationBuilder.DropIndex(
                name: "IX_RatingOptionTranslations_RatingTemplateId",
                table: "RatingOptionTranslations");

            migrationBuilder.DropColumn(
                name: "SingleChoiceTemplateId",
                table: "SingleChoiceOptionTranslations");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "RatingTemplateId",
                table: "RatingOptionTranslations");

            migrationBuilder.AlterColumn<Guid>(
                name: "SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectType",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Reviews",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Reviews",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ReviewStatus",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewType",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RatingOptionId",
                table: "RatingOptionTranslations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResponseType",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingOptionTranslations_RatingOptions_RatingOptionId",
                table: "RatingOptionTranslations",
                column: "RatingOptionId",
                principalTable: "RatingOptions",
                principalColumn: "RatingOptionId",
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
                name: "FK_SingleChoiceOptionTranslations_SingleChoiceOptions_SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewStatus",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewType",
                table: "Reviews");

            migrationBuilder.AlterColumn<Guid>(
                name: "SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "SingleChoiceTemplateId",
                table: "SingleChoiceOptionTranslations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "SubjectType",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "RatingOptionId",
                table: "RatingOptionTranslations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "RatingTemplateId",
                table: "RatingOptionTranslations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "ResponseType",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SingleChoiceOptionTranslations_SingleChoiceTemplateId",
                table: "SingleChoiceOptionTranslations",
                column: "SingleChoiceTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingOptionTranslations_RatingTemplateId",
                table: "RatingOptionTranslations",
                column: "RatingTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingOptionTranslations_RatingOptions_RatingOptionId",
                table: "RatingOptionTranslations",
                column: "RatingOptionId",
                principalTable: "RatingOptions",
                principalColumn: "RatingOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingOptionTranslations_RatingTemplates_RatingTemplateId",
                table: "RatingOptionTranslations",
                column: "RatingTemplateId",
                principalTable: "RatingTemplates",
                principalColumn: "RatingTemplateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceOptionTranslations_SingleChoiceOptions_SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations",
                column: "SingleChoiceOptionId",
                principalTable: "SingleChoiceOptions",
                principalColumn: "SingleChoiceOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceOptionTranslations_SingleChoiceTemplates_SingleChoiceTemplateId",
                table: "SingleChoiceOptionTranslations",
                column: "SingleChoiceTemplateId",
                principalTable: "SingleChoiceTemplates",
                principalColumn: "SingleChoiceTemplateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
