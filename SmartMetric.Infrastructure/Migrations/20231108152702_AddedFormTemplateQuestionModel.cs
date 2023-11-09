using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedFormTemplateQuestionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_FormTemplates_FormTemplateId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_FormTemplateId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "FormTemplateId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ReviewerType",
                table: "Questions");

            migrationBuilder.CreateTable(
                name: "FormTemplateQuestion",
                columns: table => new
                {
                    FormTemplateQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplateQuestion", x => x.FormTemplateQuestionId);
                    table.ForeignKey(
                        name: "FK_FormTemplateQuestion_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "FormTemplateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormTemplateQuestion_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplateQuestion_FormTemplateId",
                table: "FormTemplateQuestion",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplateQuestion_QuestionId",
                table: "FormTemplateQuestion",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormTemplateQuestion");

            migrationBuilder.AddColumn<Guid>(
                name: "FormTemplateId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ReviewerType",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_FormTemplateId",
                table: "Questions",
                column: "FormTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_FormTemplates_FormTemplateId",
                table: "Questions",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "FormTemplateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
