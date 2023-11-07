using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedToFormTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FormTemplates",
                columns: new[] { "FormTemplateId", "CreatedByUserId", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("8c57f6d6-2a90-4f84-b32c-5c57d86e5c20"), 1, new DateTime(2023, 11, 6, 12, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("e4ea5e10-9b3e-4a45-8b3f-7cf7417e556f"), 2, new DateTime(2023, 11, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FormTemplates",
                keyColumn: "FormTemplateId",
                keyValue: new Guid("8c57f6d6-2a90-4f84-b32c-5c57d86e5c20"));

            migrationBuilder.DeleteData(
                table: "FormTemplates",
                keyColumn: "FormTemplateId",
                keyValue: new Guid("e4ea5e10-9b3e-4a45-8b3f-7cf7417e556f"));
        }
    }
}
