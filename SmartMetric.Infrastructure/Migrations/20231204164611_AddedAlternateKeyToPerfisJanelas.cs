using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAlternateKeyToPerfisJanelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "IDPerfil",
            //    table: "PerfisJanelas",
            //    newName: "Idperfil");

            //migrationBuilder.RenameColumn(
            //    name: "IDJanela",
            //    table: "PerfisJanelas",
            //    newName: "Idjanela");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Modulo",
            //    table: "PerfisJanelas",
            //    type: "nvarchar(450)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(50)",
            //    oldMaxLength: 50,
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "Aplicacao",
            //    table: "PerfisJanelas",
            //    type: "nvarchar(450)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(50)",
            //    oldMaxLength: 50,
            //    oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerfisJanelas_Idperfil_Idjanela_Aplicacao_Modulo",
                table: "PerfisJanelas",
                columns: new[] { "Idperfil", "Idjanela", "Aplicacao", "Modulo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PerfisJanelas_Idperfil_Idjanela_Aplicacao_Modulo",
                table: "PerfisJanelas");

            //migrationBuilder.RenameColumn(
            //    name: "Idperfil",
            //    table: "PerfisJanelas",
            //    newName: "IDPerfil");

            //migrationBuilder.RenameColumn(
            //    name: "Idjanela",
            //    table: "PerfisJanelas",
            //    newName: "IDJanela");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Modulo",
            //    table: "PerfisJanelas",
            //    type: "nvarchar(50)",
            //    maxLength: 50,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "Aplicacao",
            //    table: "PerfisJanelas",
            //    type: "nvarchar(50)",
            //    maxLength: 50,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);
        }
    }
}
