using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefacotryToEntitiesReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_ReviewResponses_Metrics_Questions_QuestionId",
                table: "Metrics_ReviewResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_ReviewResponses_Metrics_SingleChoiceOptions_SingleChoiceOptionId",
                table: "Metrics_ReviewResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Submissions_Metrics_Reviews_ReviewId",
                table: "Metrics_Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Metrics_ReviewResponses_QuestionId",
                table: "Metrics_ReviewResponses");

            migrationBuilder.DropIndex(
                name: "IX_Metrics_ReviewResponses_SingleChoiceOptionId",
                table: "Metrics_ReviewResponses");

            migrationBuilder.DropColumn(
                name: "SingleChoiceOptionId",
                table: "Metrics_ReviewResponses");

            migrationBuilder.RenameColumn(
                name: "RatingValue",
                table: "Metrics_ReviewResponses",
                newName: "RatingValueResponse");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "Metrics_ReviewResponses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "Metrics_ReviewDepartments",
                columns: table => new
                {
                    ReviewDepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics_ReviewDepartments", x => x.ReviewDepartmentId);
                    table.ForeignKey(
                        name: "FK_Metrics_ReviewDepartments_Departamentos_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departamentos",
                        principalColumn: "IDDepartamento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Metrics_ReviewDepartments_Metrics_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Metrics_Reviews",
                        principalColumn: "ReviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Metrics_ReviewEmployees",
                columns: table => new
                {
                    ReviewEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics_ReviewEmployees", x => x.ReviewEmployeeId);
                    table.ForeignKey(
                        name: "FK_Metrics_ReviewEmployees_Funcionarios_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Funcionarios",
                        principalColumn: "IDFuncionario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Metrics_ReviewEmployees_Metrics_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Metrics_Reviews",
                        principalColumn: "ReviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Metrics_ReviewTranslation",
                columns: table => new
                {
                    ReviewTranslationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics_ReviewTranslation", x => x.ReviewTranslationId);
                    table.ForeignKey(
                        name: "FK_Metrics_ReviewTranslation_Metrics_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Metrics_Reviews",
                        principalColumn: "ReviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_Submissions_EvaluatedEmployeeId",
                table: "Metrics_Submissions",
                column: "EvaluatedEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_Submissions_EvaluatorEmployeeId",
                table: "Metrics_Submissions",
                column: "EvaluatorEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_Reviews_CreatedByUserId",
                table: "Metrics_Reviews",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_ReviewDepartments_DepartmentId",
                table: "Metrics_ReviewDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_ReviewDepartments_ReviewId",
                table: "Metrics_ReviewDepartments",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_ReviewEmployees_EmployeeId",
                table: "Metrics_ReviewEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_ReviewEmployees_ReviewId",
                table: "Metrics_ReviewEmployees",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_ReviewTranslation_ReviewId",
                table: "Metrics_ReviewTranslation",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_Reviews_Funcionarios_CreatedByUserId",
                table: "Metrics_Reviews",
                column: "CreatedByUserId",
                principalTable: "Funcionarios",
                principalColumn: "IDFuncionario");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_Submissions_Funcionarios_EvaluatedEmployeeId",
                table: "Metrics_Submissions",
                column: "EvaluatedEmployeeId",
                principalTable: "Funcionarios",
                principalColumn: "IDFuncionario");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_Submissions_Funcionarios_EvaluatorEmployeeId",
                table: "Metrics_Submissions",
                column: "EvaluatorEmployeeId",
                principalTable: "Funcionarios",
                principalColumn: "IDFuncionario");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_Submissions_Metrics_Reviews_ReviewId",
                table: "Metrics_Submissions",
                column: "ReviewId",
                principalTable: "Metrics_Reviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Reviews_Funcionarios_CreatedByUserId",
                table: "Metrics_Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Submissions_Funcionarios_EvaluatedEmployeeId",
                table: "Metrics_Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Submissions_Funcionarios_EvaluatorEmployeeId",
                table: "Metrics_Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Submissions_Metrics_Reviews_ReviewId",
                table: "Metrics_Submissions");

            migrationBuilder.DropTable(
                name: "Metrics_ReviewDepartments");

            migrationBuilder.DropTable(
                name: "Metrics_ReviewEmployees");

            migrationBuilder.DropTable(
                name: "Metrics_ReviewTranslation");

            migrationBuilder.DropIndex(
                name: "IX_Metrics_Submissions_EvaluatedEmployeeId",
                table: "Metrics_Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Metrics_Submissions_EvaluatorEmployeeId",
                table: "Metrics_Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Metrics_Reviews_CreatedByUserId",
                table: "Metrics_Reviews");

            migrationBuilder.RenameColumn(
                name: "RatingValueResponse",
                table: "Metrics_ReviewResponses",
                newName: "RatingValue");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "Metrics_ReviewResponses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SingleChoiceOptionId",
                table: "Metrics_ReviewResponses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_ReviewResponses_QuestionId",
                table: "Metrics_ReviewResponses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_ReviewResponses_SingleChoiceOptionId",
                table: "Metrics_ReviewResponses",
                column: "SingleChoiceOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_ReviewResponses_Metrics_Questions_QuestionId",
                table: "Metrics_ReviewResponses",
                column: "QuestionId",
                principalTable: "Metrics_Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_ReviewResponses_Metrics_SingleChoiceOptions_SingleChoiceOptionId",
                table: "Metrics_ReviewResponses",
                column: "SingleChoiceOptionId",
                principalTable: "Metrics_SingleChoiceOptions",
                principalColumn: "SingleChoiceOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_Submissions_Metrics_Reviews_ReviewId",
                table: "Metrics_Submissions",
                column: "ReviewId",
                principalTable: "Metrics_Reviews",
                principalColumn: "ReviewId");
        }
    }
}
