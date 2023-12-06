using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedModifiedDateToReviewEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Metrics_Reviews",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Metrics_Reviews");
        }
    }
}
