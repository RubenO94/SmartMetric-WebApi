using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactoryToReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Reviews_Funcionarios_CreatedByUserId",
                table: "Metrics_Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_Reviews_Utilizadores_CreatedByUserId",
                table: "Metrics_Reviews",
                column: "CreatedByUserId",
                principalTable: "Utilizadores",
                principalColumn: "IDUtilizador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Reviews_Utilizadores_CreatedByUserId",
                table: "Metrics_Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_Reviews_Funcionarios_CreatedByUserId",
                table: "Metrics_Reviews",
                column: "CreatedByUserId",
                principalTable: "Funcionarios",
                principalColumn: "IDFuncionario");
        }
    }
}
