using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormTemplates",
                columns: table => new
                {
                    FormTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplates", x => x.FormTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "RatingTemplates",
                columns: table => new
                {
                    RatingTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false)
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
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleChoiceTemplates", x => x.SingleChoiceTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "FormTemplateTranslations",
                columns: table => new
                {
                    FormTemplateTranslationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplateTranslations", x => x.FormTemplateTranslationId);
                    table.ForeignKey(
                        name: "FK_FormTemplateTranslations_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "FormTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SubjectType = table.Column<int>(type: "int", nullable: false),
                    EvaluatorEmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "FormTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingOptions",
                columns: table => new
                {
                    RatingOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumericValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingOptions", x => x.RatingOptionId);
                    table.ForeignKey(
                        name: "FK_RatingOptions_RatingTemplates_RatingTemplateId",
                        column: x => x.RatingTemplateId,
                        principalTable: "RatingTemplates",
                        principalColumn: "RatingTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingTemplateTranslations",
                columns: table => new
                {
                    RatingTemplateTranslationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SingleChoiceTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    ResponseType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "FormTemplateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_RatingTemplates_RatingTemplateId",
                        column: x => x.RatingTemplateId,
                        principalTable: "RatingTemplates",
                        principalColumn: "RatingTemplateId");
                    table.ForeignKey(
                        name: "FK_Questions_SingleChoiceTemplates_SingleChoiceTemplateId",
                        column: x => x.SingleChoiceTemplateId,
                        principalTable: "SingleChoiceTemplates",
                        principalColumn: "SingleChoiceTemplateId");
                });

            migrationBuilder.CreateTable(
                name: "SingleChoiceOptions",
                columns: table => new
                {
                    SingleChoiceOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SingleChoiceTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleChoiceOptions", x => x.SingleChoiceOptionId);
                    table.ForeignKey(
                        name: "FK_SingleChoiceOptions_SingleChoiceTemplates_SingleChoiceTemplateId",
                        column: x => x.SingleChoiceTemplateId,
                        principalTable: "SingleChoiceTemplates",
                        principalColumn: "SingleChoiceTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingleChoiceTemplateTranslations",
                columns: table => new
                {
                    SingleChoiceTemplateTranslationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SingleChoiceTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    SubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.SubmissionId);
                    table.ForeignKey(
                        name: "FK_Submissions_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "ReviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingOptionTranslations",
                columns: table => new
                {
                    RatingOptionTranslationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatingOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingOptionTranslations", x => x.RatingOptionTranslationId);
                    table.ForeignKey(
                        name: "FK_RatingOptionTranslations_RatingOptions_RatingOptionId",
                        column: x => x.RatingOptionId,
                        principalTable: "RatingOptions",
                        principalColumn: "RatingOptionId");
                    table.ForeignKey(
                        name: "FK_RatingOptionTranslations_RatingTemplates_RatingTemplateId",
                        column: x => x.RatingTemplateId,
                        principalTable: "RatingTemplates",
                        principalColumn: "RatingTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTranslations",
                columns: table => new
                {
                    QuestionTranslationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTranslations", x => x.QuestionTranslationId);
                    table.ForeignKey(
                        name: "FK_QuestionTranslations_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingleChoiceOptionTranslations",
                columns: table => new
                {
                    SingleChoiceOptionTranslationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SingleChoiceTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SingleChoiceOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleChoiceOptionTranslations", x => x.SingleChoiceOptionTranslationId);
                    table.ForeignKey(
                        name: "FK_SingleChoiceOptionTranslations_SingleChoiceOptions_SingleChoiceOptionId",
                        column: x => x.SingleChoiceOptionId,
                        principalTable: "SingleChoiceOptions",
                        principalColumn: "SingleChoiceOptionId");
                    table.ForeignKey(
                        name: "FK_SingleChoiceOptionTranslations_SingleChoiceTemplates_SingleChoiceTemplateId",
                        column: x => x.SingleChoiceTemplateId,
                        principalTable: "SingleChoiceTemplates",
                        principalColumn: "SingleChoiceTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewResponses",
                columns: table => new
                {
                    ReviewResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SingleChoiceOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TextResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatingValue = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewResponses", x => x.ReviewResponseId);
                    table.ForeignKey(
                        name: "FK_ReviewResponses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewResponses_SingleChoiceOptions_SingleChoiceOptionId",
                        column: x => x.SingleChoiceOptionId,
                        principalTable: "SingleChoiceOptions",
                        principalColumn: "SingleChoiceOptionId");
                    table.ForeignKey(
                        name: "FK_ReviewResponses_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "SubmissionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplateTranslations_FormTemplateId",
                table: "FormTemplateTranslations",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_FormTemplateId",
                table: "Questions",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_RatingTemplateId",
                table: "Questions",
                column: "RatingTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SingleChoiceTemplateId",
                table: "Questions",
                column: "SingleChoiceTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTranslations_QuestionId",
                table: "QuestionTranslations",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingOptions_RatingTemplateId",
                table: "RatingOptions",
                column: "RatingTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingOptionTranslations_RatingOptionId",
                table: "RatingOptionTranslations",
                column: "RatingOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingOptionTranslations_RatingTemplateId",
                table: "RatingOptionTranslations",
                column: "RatingTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingTemplateTranslations_RatingTemplateId",
                table: "RatingTemplateTranslations",
                column: "RatingTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewResponses_QuestionId",
                table: "ReviewResponses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewResponses_SingleChoiceOptionId",
                table: "ReviewResponses",
                column: "SingleChoiceOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewResponses_SubmissionId",
                table: "ReviewResponses",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_FormTemplateId",
                table: "Reviews",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChoiceOptions_SingleChoiceTemplateId",
                table: "SingleChoiceOptions",
                column: "SingleChoiceTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChoiceOptionTranslations_SingleChoiceOptionId",
                table: "SingleChoiceOptionTranslations",
                column: "SingleChoiceOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChoiceOptionTranslations_SingleChoiceTemplateId",
                table: "SingleChoiceOptionTranslations",
                column: "SingleChoiceTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleChoiceTemplateTranslations_SingleChoiceTemplateId",
                table: "SingleChoiceTemplateTranslations",
                column: "SingleChoiceTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ReviewId",
                table: "Submissions",
                column: "ReviewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormTemplateTranslations");

            migrationBuilder.DropTable(
                name: "QuestionTranslations");

            migrationBuilder.DropTable(
                name: "RatingOptionTranslations");

            migrationBuilder.DropTable(
                name: "RatingTemplateTranslations");

            migrationBuilder.DropTable(
                name: "ReviewResponses");

            migrationBuilder.DropTable(
                name: "SingleChoiceOptionTranslations");

            migrationBuilder.DropTable(
                name: "SingleChoiceTemplateTranslations");

            migrationBuilder.DropTable(
                name: "RatingOptions");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "SingleChoiceOptions");

            migrationBuilder.DropTable(
                name: "RatingTemplates");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SingleChoiceTemplates");

            migrationBuilder.DropTable(
                name: "FormTemplates");
        }
    }
}
