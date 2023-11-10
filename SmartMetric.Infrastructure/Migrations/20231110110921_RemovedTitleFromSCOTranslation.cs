using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTitleFromSCOTranslation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "SingleChoiceOptionTranslations");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SingleChoiceOptionTranslations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SingleChoiceOptionTranslations",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "SingleChoiceOptionTranslations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
