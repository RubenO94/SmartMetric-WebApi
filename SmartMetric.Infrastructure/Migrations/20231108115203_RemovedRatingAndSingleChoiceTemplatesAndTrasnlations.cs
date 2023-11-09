using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRatingAndSingleChoiceTemplatesAndTrasnlations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_RatingTemplates_RatingTemplateId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_SingleChoiceTemplates_SingleChoiceTemplateId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingOptions_RatingTemplates_RatingTemplateId",
                table: "RatingOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceOptions_SingleChoiceTemplates_SingleChoiceTemplateId",
                table: "SingleChoiceOptions");

            migrationBuilder.DropTable(
                name: "RatingTemplateTranslations");

            migrationBuilder.DropTable(
                name: "SingleChoiceTemplateTranslations");

            migrationBuilder.DropTable(
                name: "RatingTemplates");

            migrationBuilder.DropTable(
                name: "SingleChoiceTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Questions_RatingTemplateId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SingleChoiceTemplateId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "RatingTemplateId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "SingleChoiceTemplateId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "SingleChoiceTemplateId",
                table: "SingleChoiceOptions",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_SingleChoiceOptions_SingleChoiceTemplateId",
                table: "SingleChoiceOptions",
                newName: "IX_SingleChoiceOptions_QuestionId");

            migrationBuilder.RenameColumn(
                name: "RatingTemplateId",
                table: "RatingOptions",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_RatingOptions_RatingTemplateId",
                table: "RatingOptions",
                newName: "IX_RatingOptions_QuestionId");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReviewerType",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingOptions_Questions_QuestionId",
                table: "RatingOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceOptions_Questions_QuestionId",
                table: "SingleChoiceOptions",
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

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceOptions_Questions_QuestionId",
                table: "SingleChoiceOptions");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewerType",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "SingleChoiceOptions",
                newName: "SingleChoiceTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_SingleChoiceOptions_QuestionId",
                table: "SingleChoiceOptions",
                newName: "IX_SingleChoiceOptions_SingleChoiceTemplateId");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "RatingOptions",
                newName: "RatingTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_RatingOptions_QuestionId",
                table: "RatingOptions",
                newName: "IX_RatingOptions_RatingTemplateId");

            migrationBuilder.AddColumn<Guid>(
                name: "RatingTemplateId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SingleChoiceTemplateId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RatingTemplates",
                columns: table => new
                {
                    RatingTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingTemplates", x => x.RatingTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "SingleChoiceTemplates",
                columns: table => new
                {
                    SingleChoiceTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleChoiceTemplates", x => x.SingleChoiceTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "RatingTemplateTranslations",
                columns: table => new
                {
                    RatingTemplateTranslationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingTemplateTranslations", x => x.RatingTemplateTranslationId);
                    table.ForeignKey(
                        name: "FK_RatingTemplateTranslations_RatingTemplates_RatingTemplateId",
                        column: x => x.RatingTemplateId,
                        principalTable: "RatingTemplates",
                        principalColumn: "RatingTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingleChoiceTemplateTranslations",
                columns: table => new
                {
                    SingleChoiceTemplateTranslationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SingleChoiceTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleChoiceTemplateTranslations", x => x.SingleChoiceTemplateTranslationId);
                    table.ForeignKey(
                        name: "FK_SingleChoiceTemplateTranslations_SingleChoiceTemplates_SingleChoiceTemplateId",
                        column: x => x.SingleChoiceTemplateId,
                        principalTable: "SingleChoiceTemplates",
                        principalColumn: "SingleChoiceTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_RatingTemplateId",
                table: "Questions",
                column: "RatingTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SingleChoiceTemplateId",
                table: "Questions",
                column: "SingleChoiceTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingTemplateTranslations_RatingTemplateId",
                table: "RatingTemplateTranslations",
                column: "RatingTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChoiceTemplateTranslations_SingleChoiceTemplateId",
                table: "SingleChoiceTemplateTranslations",
                column: "SingleChoiceTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_RatingTemplates_RatingTemplateId",
                table: "Questions",
                column: "RatingTemplateId",
                principalTable: "RatingTemplates",
                principalColumn: "RatingTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_SingleChoiceTemplates_SingleChoiceTemplateId",
                table: "Questions",
                column: "SingleChoiceTemplateId",
                principalTable: "SingleChoiceTemplates",
                principalColumn: "SingleChoiceTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingOptions_RatingTemplates_RatingTemplateId",
                table: "RatingOptions",
                column: "RatingTemplateId",
                principalTable: "RatingTemplates",
                principalColumn: "RatingTemplateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceOptions_SingleChoiceTemplates_SingleChoiceTemplateId",
                table: "SingleChoiceOptions",
                column: "SingleChoiceTemplateId",
                principalTable: "SingleChoiceTemplates",
                principalColumn: "SingleChoiceTemplateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
