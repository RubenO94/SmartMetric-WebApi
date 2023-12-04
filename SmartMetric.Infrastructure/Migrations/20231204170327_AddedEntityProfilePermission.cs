using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedEntityProfilePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metrics_ProfilePermissions",
                columns: table => new
                {
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics_ProfilePermissions", x => new { x.ProfileId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_Metrics_ProfilePermissions_Perfis_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Perfis",
                        principalColumn: "IDPerfil",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metrics_ProfilePermissions");
        }
    }
}
