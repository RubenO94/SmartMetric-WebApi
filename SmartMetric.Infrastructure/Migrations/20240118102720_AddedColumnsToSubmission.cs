using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnsToSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvaluatedDepartmentId",
                table: "Metrics_Submissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvaluatorDepartmentId",
                table: "Metrics_Submissions",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvaluatedDepartmentId",
                table: "Metrics_Submissions");

            migrationBuilder.DropColumn(
                name: "EvaluatorDepartmentId",
                table: "Metrics_Submissions");
        }
    }
}
