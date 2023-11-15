using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeToFormTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplateTranslations_FormTemplates_FormTemplateId",
                table: "FormTemplateTranslations");

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplateTranslations_FormTemplates_FormTemplateId",
                table: "FormTemplateTranslations",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "FormTemplateId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormTemplateTranslations_FormTemplates_FormTemplateId",
                table: "FormTemplateTranslations");

            migrationBuilder.AddForeignKey(
                name: "FK_FormTemplateTranslations_FormTemplates_FormTemplateId",
                table: "FormTemplateTranslations",
                column: "FormTemplateId",
                principalTable: "FormTemplates",
                principalColumn: "FormTemplateId");
        }
    }
}
