using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPerfisAndUtilizadoresTabels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Perfis",
            //    columns: table => new
            //    {
            //        IDPerfil = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nome = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
            //        Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        PortalColaborador = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("IDPerfil", x => x.IDPerfil);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PerfisDepartamentos",
            //    columns: table => new
            //    {
            //        IDPerfil = table.Column<int>(type: "int", nullable: true),
            //        IDDepartamento = table.Column<int>(type: "int", nullable: true),
            //        NivelFerias = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
            //        NivelAusencias = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
            //        NivelExtras = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
            //        NivelFuncionariosMarcacoes = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
            //        NivelAusenciasServico = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
            //        Nivel = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PerfisJanelas",
            //    columns: table => new
            //    {
            //        IDPerfil = table.Column<int>(type: "int", nullable: true),
            //        IDJanela = table.Column<int>(type: "int", nullable: true),
            //        Aplicacao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Modulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Utilizadores",
            //    columns: table => new
            //    {
            //        IDUtilizador = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        IDPerfil = table.Column<int>(type: "int", nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Ativo = table.Column<bool>(type: "bit", nullable: true),
            //        UltimoAcessoSmartTime = table.Column<DateTime>(type: "datetime", nullable: true),
            //        IDFuncionario = table.Column<int>(type: "int", nullable: true),
            //        Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Activo = table.Column<bool>(type: "bit", nullable: true),
            //        TentativasAcesso = table.Column<int>(type: "int", nullable: true),
            //        UltimoAcessoSmartAccess = table.Column<DateTime>(type: "datetime", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("IDUtilizador", x => x.IDUtilizador);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_PerfisDepartamentos_IDPerfil",
            //    table: "PerfisDepartamentos",
            //    column: "IDPerfil")
            //    .Annotation("SqlServer:FillFactor", 90);

            //migrationBuilder.CreateIndex(
            //    name: "IX_PerfisJanelas_IDPerfil_Aplicacao",
            //    table: "PerfisJanelas",
            //    columns: new[] { "IDPerfil", "Aplicacao" })
            //    .Annotation("SqlServer:FillFactor", 90);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Perfis");

            //migrationBuilder.DropTable(
            //    name: "PerfisDepartamentos");

            //migrationBuilder.DropTable(
            //    name: "PerfisJanelas");

            //migrationBuilder.DropTable(
            //    name: "Utilizadores");
        }
    }
}
