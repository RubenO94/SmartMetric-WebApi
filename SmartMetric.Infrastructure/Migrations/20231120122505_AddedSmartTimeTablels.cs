using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSmartTimeTablels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    IDDepartamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IDDepartamentoPai = table.Column<int>(type: "int", nullable: true),
                    IDEntidade = table.Column<int>(type: "int", nullable: true),
                    Notas = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NumeroFuncionariosFerias = table.Column<int>(type: "int", nullable: true),
                    MaximoFuncionariosFerias = table.Column<int>(type: "int", nullable: true),
                    EmailChefia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NaoValidarDocumentos = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IDDepartamento", x => x.IDDepartamento);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    IDFuncionario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: true),
                    Morada = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Morada2 = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Localidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IDDepartamento = table.Column<int>(type: "int", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Telemovel = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Fotografia = table.Column<byte[]>(type: "image", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Notas = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Cartao = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    CartaoAlternativo = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    DataAdmissao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataDemissao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime", nullable: true),
                    CentroCusto = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    NomeAbreviado = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IDPlanoHorarios = table.Column<int>(type: "int", nullable: true),
                    IDMunicipio = table.Column<int>(type: "int", nullable: true),
                    HorasSemanais = table.Column<DateTime>(type: "datetime", nullable: true),
                    PerfilAcesso = table.Column<int>(type: "int", nullable: true),
                    IDEntidade = table.Column<int>(type: "int", nullable: true),
                    AdministradorTerminais = table.Column<bool>(type: "bit", nullable: true),
                    AcessoPortalWeb = table.Column<bool>(type: "bit", nullable: true),
                    LoginLDAP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UltimoAcessoSmartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    SuperiorHierarquico = table.Column<bool>(type: "bit", nullable: true),
                    IDPerfil = table.Column<int>(type: "int", nullable: true),
                    NumeroFilhos = table.Column<int>(type: "int", nullable: true),
                    ValorHora = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorPremioAssiduidade = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubAlimentacao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NaoExportar = table.Column<bool>(type: "bit", nullable: true),
                    ValorBase = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NaoGerarSubTurno = table.Column<bool>(type: "bit", nullable: true),
                    HorasMensais = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeviceID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IDGrupo = table.Column<int>(type: "int", nullable: true),
                    MarcarSemBiometria = table.Column<bool>(type: "bit", nullable: true),
                    ValorSubsidio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CST_FullControl = table.Column<bool>(type: "bit", nullable: true),
                    DataExpiracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    PINAPK = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PINAPP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Local = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PermitirFeriasAnoAnterior = table.Column<bool>(type: "bit", nullable: true),
                    Alarme = table.Column<bool>(type: "bit", nullable: true),
                    TentativasAcesso = table.Column<int>(type: "int", nullable: true),
                    GDPR = table.Column<bool>(type: "bit", nullable: true),
                    Idioma = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IgnorarPrazoAntecedenciaExtras = table.Column<bool>(type: "bit", nullable: true),
                    PrimeiroAcesso = table.Column<bool>(type: "bit", nullable: true),
                    SuperiorHierarquicoPorDelegacao = table.Column<bool>(type: "bit", nullable: true),
                    SoAcessos = table.Column<bool>(type: "bit", nullable: true),
                    EmailEquipa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IDPerfilEquipa = table.Column<int>(type: "int", nullable: true),
                    IDPerfilEquipaAcesso = table.Column<int>(type: "int", nullable: true),
                    ValorDiario = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Temporario = table.Column<bool>(type: "bit", nullable: true),
                    CartaoAlternativo1 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Sexo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    EstadoCivil = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Assinatura = table.Column<byte[]>(type: "image", nullable: true),
                    IDPerfilSuperiorHierarquico = table.Column<int>(type: "int", nullable: true),
                    PlanosHorariosLinha = table.Column<int>(type: "int", nullable: true),
                    OcultarBH = table.Column<bool>(type: "bit", nullable: true),
                    NaoGerarSubAlimentacao = table.Column<bool>(type: "bit", nullable: true),
                    EnviarOffline = table.Column<bool>(type: "bit", nullable: true),
                    ValorHoraFDS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NaoValidarDocumentos = table.Column<bool>(type: "bit", nullable: true),
                    IgnorarLimites = table.Column<bool>(type: "bit", nullable: true),
                    Folha = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CartaConducao = table.Column<bool>(type: "bit", nullable: true),
                    IDPersonAPP = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Seguranca = table.Column<bool>(type: "bit", nullable: true),
                    Visitado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IDFuncionario", x => x.IDFuncionario);
                });

            migrationBuilder.CreateTable(
                name: "FuncionariosChefias",
                columns: table => new
                {
                    IDFuncionarioSuperior = table.Column<int>(type: "int", nullable: true),
                    IDFuncionario = table.Column<int>(type: "int", nullable: true),
                    IDDepartamento = table.Column<int>(type: "int", nullable: true),
                    NivelFerias = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    NivelAusencias = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    NivelBH = table.Column<int>(type: "int", nullable: true),
                    NivelExtras = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    NivelFuncionariosMarcacoes = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    NivelAusenciasServico = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Nivel = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_IDEntidade",
                table: "Departamentos",
                column: "IDEntidade")
                .Annotation("SqlServer:FillFactor", 90);

            migrationBuilder.CreateIndex(
                name: "_dta_index_Funcionarios_7_962102468__K1",
                table: "Funcionarios",
                column: "IDFuncionario");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_DeviceID",
                table: "Funcionarios",
                column: "DeviceID")
                .Annotation("SqlServer:FillFactor", 90);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_IDEntidade",
                table: "Funcionarios",
                column: "IDEntidade")
                .Annotation("SqlServer:FillFactor", 90);

            migrationBuilder.CreateIndex(
                name: "IX_FuncionariosChefias_IDDepartamento",
                table: "FuncionariosChefias",
                column: "IDDepartamento")
                .Annotation("SqlServer:FillFactor", 100);

            migrationBuilder.CreateIndex(
                name: "IX_FuncionariosChefias_IDFuncionario",
                table: "FuncionariosChefias",
                column: "IDFuncionario")
                .Annotation("SqlServer:FillFactor", 100);

            migrationBuilder.CreateIndex(
                name: "IX_FuncionariosChefias_IDFuncionarioSuperior",
                table: "FuncionariosChefias",
                column: "IDFuncionarioSuperior")
                .Annotation("SqlServer:FillFactor", 100);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "FuncionariosChefias");
        }
    }
}
