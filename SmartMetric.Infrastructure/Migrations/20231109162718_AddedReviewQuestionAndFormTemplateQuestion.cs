using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedReviewQuestionAndFormTemplateQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplateQuestion_FormTemplates_FormTemplateId",
                table: "FormTemplateQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplateQuestion_Questions_QuestionId",
                table: "FormTemplateQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplateTranslations_FormTemplates_FormTemplateId",
                table: "FormTemplateTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewResponses_Questions_QuestionId",
                table: "ReviewResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_FormTemplates_FormTemplateId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceOptions_Questions_QuestionId",
                table: "SingleChoiceOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceOptionTranslations_SingleChoiceOptions_SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Reviews_ReviewId",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormTemplateQuestion",
                table: "FormTemplateQuestion");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "RatingOptionTranslations");

            migrationBuilder.RenameTable(
                name: "FormTemplateQuestion",
                newName: "FormTemplateQuestions");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "ReviewResponses",
                newName: "ReviewQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewResponses_QuestionId",
                table: "ReviewResponses",
                newName: "IX_ReviewResponses_ReviewQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_FormTemplateQuestion_QuestionId",
                table: "FormTemplateQuestions",
                newName: "IX_FormTemplateQuestions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_FormTemplateQuestion_FormTemplateId",
                table: "FormTemplateQuestions",
                newName: "IX_FormTemplateQuestions_FormTemplateId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmissionDate",
                table: "Submissions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReviewId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "EvaluatorEmployeeId",
                table: "Submissions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EvaluatedEmployeeId",
                table: "Submissions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "SingleChoiceOptionTranslations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "SingleChoiceOptionTranslations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SingleChoiceOptionTranslations",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "SingleChoiceOptions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectType",
                table: "Reviews",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReviewType",
                table: "Reviews",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReviewStatus",
                table: "Reviews",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FormTemplateId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Reviews",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TextResponse",
                table: "ReviewResponses",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "RatingOptionTranslations",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RatingOptionTranslations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "QuestionTranslations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "QuestionTranslations",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "QuestionTranslations",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResponseType",
                table: "Questions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "FormTemplateTranslations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "FormTemplateTranslations",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FormTemplateId",
                table: "FormTemplateTranslations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FormTemplateTranslations",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "FormTemplates",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "FormTemplates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormTemplateQuestions",
                table: "FormTemplateQuestions",
                column: "FormTemplateQuestionId");

            migrationBuilder.CreateTable(
                name: "ReviewsQuestions",
                columns: table => new
                {
                    ReviewQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsQuestions_QuestionId",
                table: "ReviewsQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsQuestions_ReviewId",
                table: "ReviewsQuestions",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplateQuestions_FormTemplates_FormTemplateId",
                table: "FormTemplateQuestions",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "FormTemplateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplateQuestions_Questions_QuestionId",
                table: "FormTemplateQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplateTranslations_FormTemplates_FormTemplateId",
                table: "FormTemplateTranslations",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "FormTemplateId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceOptions_Questions_QuestionId",
                table: "SingleChoiceOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceOptionTranslations_SingleChoiceOptions_SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations",
                column: "SingleChoiceOptionId",
                principalTable: "SingleChoiceOptions",
                principalColumn: "SingleChoiceOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Reviews_ReviewId",
                table: "Submissions",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplateQuestions_FormTemplates_FormTemplateId",
                table: "FormTemplateQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplateQuestions_Questions_QuestionId",
                table: "FormTemplateQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplateTranslations_FormTemplates_FormTemplateId",
                table: "FormTemplateTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewResponses_ReviewsQuestions_ReviewQuestionId",
                table: "ReviewResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_FormTemplates_FormTemplateId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceOptions_Questions_QuestionId",
                table: "SingleChoiceOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleChoiceOptionTranslations_SingleChoiceOptions_SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Reviews_ReviewId",
                table: "Submissions");

            migrationBuilder.DropTable(
                name: "ReviewsQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormTemplateQuestions",
                table: "FormTemplateQuestions");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "FormTemplateQuestions",
                newName: "FormTemplateQuestion");

            migrationBuilder.RenameColumn(
                name: "ReviewQuestionId",
                table: "ReviewResponses",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewResponses_ReviewQuestionId",
                table: "ReviewResponses",
                newName: "IX_ReviewResponses_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_FormTemplateQuestions_QuestionId",
                table: "FormTemplateQuestion",
                newName: "IX_FormTemplateQuestion_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_FormTemplateQuestions_FormTemplateId",
                table: "FormTemplateQuestion",
                newName: "IX_FormTemplateQuestion_FormTemplateId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmissionDate",
                table: "Submissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReviewId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EvaluatorEmployeeId",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EvaluatedEmployeeId",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "SingleChoiceOptionTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

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
                name: "Language",
                table: "SingleChoiceOptionTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SingleChoiceOptionTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "SingleChoiceOptions",
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
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReviewType",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReviewStatus",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FormTemplateId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TextResponse",
                table: "ReviewResponses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "RatingOptionTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RatingOptionTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "RatingOptionTranslations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "QuestionTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "QuestionTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "QuestionTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResponseType",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "FormTemplateTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "FormTemplateTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FormTemplateId",
                table: "FormTemplateTranslations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FormTemplateTranslations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "FormTemplates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "FormTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormTemplateQuestion",
                table: "FormTemplateQuestion",
                column: "FormTemplateQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplateQuestion_FormTemplates_FormTemplateId",
                table: "FormTemplateQuestion",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "FormTemplateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplateQuestion_Questions_QuestionId",
                table: "FormTemplateQuestion",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplateTranslations_FormTemplates_FormTemplateId",
                table: "FormTemplateTranslations",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "FormTemplateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewResponses_Questions_QuestionId",
                table: "ReviewResponses",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_FormTemplates_FormTemplateId",
                table: "Reviews",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "FormTemplateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceOptions_Questions_QuestionId",
                table: "SingleChoiceOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleChoiceOptionTranslations_SingleChoiceOptions_SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations",
                column: "SingleChoiceOptionId",
                principalTable: "SingleChoiceOptions",
                principalColumn: "SingleChoiceOptionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Reviews_ReviewId",
                table: "Submissions",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
