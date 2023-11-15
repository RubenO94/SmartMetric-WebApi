using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewResponses_ReviewsQuestions_ReviewQuestionId",
                table: "ReviewResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_FormTemplates_FormTemplateId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "FormTemplateQuestions");

            migrationBuilder.DropTable(
                name: "ReviewsQuestions");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_FormTemplateId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "FormTemplateId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "ReviewQuestionId",
                table: "ReviewResponses",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewResponses_ReviewQuestionId",
                table: "ReviewResponses",
                newName: "IX_ReviewResponses_QuestionId");

            migrationBuilder.AddColumn<Guid>(
                name: "FormTemplateId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReviewId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"),
                columns: new[] { "FormTemplateId", "ReviewId" },
                values: new object[] { new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), null });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb1"),
                columns: new[] { "FormTemplateId", "ReviewId" },
                values: new object[] { new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), null });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_FormTemplateId",
                table: "Questions",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ReviewId",
                table: "Questions",
                column: "ReviewId");

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
                name: "FK_ReviewResponses_Questions_QuestionId",
                table: "ReviewResponses",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_FormTemplates_FormTemplateId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Reviews_ReviewId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewResponses_Questions_QuestionId",
                table: "ReviewResponses");

            migrationBuilder.DropIndex(
                name: "IX_Questions_FormTemplateId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ReviewId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "FormTemplateId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "ReviewResponses",
                newName: "ReviewQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewResponses_QuestionId",
                table: "ReviewResponses",
                newName: "IX_ReviewResponses_ReviewQuestionId");

            migrationBuilder.AddColumn<Guid>(
                name: "FormTemplateId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FormTemplateQuestions",
                columns: table => new
                {
                    FormTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplateQuestions", x => new { x.FormTemplateId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_FormTemplateQuestions_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "FormTemplateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormTemplateQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewsQuestions",
                columns: table => new
                {
                    ReviewQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewsQuestions", x => x.ReviewQuestionId);
                    table.ForeignKey(
                        name: "FK_ReviewsQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewsQuestions_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "ReviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FormTemplateQuestions",
                columns: new[] { "FormTemplateId", "QuestionId" },
                values: new object[,]
                {
                    { new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8") },
                    { new Guid("8f7f0f64-5317-4562-b3fc-2c963f66afa6"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb1") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_FormTemplateId",
                table: "Reviews",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplateQuestions_QuestionId",
                table: "FormTemplateQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsQuestions_QuestionId",
                table: "ReviewsQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsQuestions_ReviewId",
                table: "ReviewsQuestions",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewResponses_ReviewsQuestions_ReviewQuestionId",
                table: "ReviewResponses",
                column: "ReviewQuestionId",
                principalTable: "ReviewsQuestions",
                principalColumn: "ReviewQuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_FormTemplates_FormTemplateId",
                table: "Reviews",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "FormTemplateId");
        }
    }
}
