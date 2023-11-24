using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Infrastructure.DatabaseContext
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #region Entities

        public DbSet<FormTemplate> FormTemplates { get; set; }
        public DbSet<FormTemplateTranslation> FormTemplateTranslations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionTranslation> QuestionTranslations { get; set; }
        public DbSet<RatingOption> RatingOptions { get; set; }
        public DbSet<RatingOptionTranslation> RatingOptionTranslations { get; set; }
        public DbSet<SingleChoiceOption> SingleChoiceOptions { get; set; }
        public DbSet<SingleChoiceOptionTranslation> SingleChoiceOptionTranslations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewResponse> ReviewResponses { get; set; }
        public DbSet<ReviewEmployee> ReviewEmployees { get; set; }
        public DbSet<ReviewDepartment> ReviewDepartments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<Funcionario> Funcionarios { get; set; }
        public virtual DbSet<FuncionariosChefia> FuncionariosChefias { get; set; }
        public virtual DbSet<Perfis> Perfis { get; set; }
        public virtual DbSet<PerfisDepartamento> PerfisDepartamentos { get; set; }
        public virtual DbSet<PerfisJanela> PerfisJanelas { get; set; }
        public virtual DbSet<Utilizador> Utilizadores { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Adicionar Prefixos

            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                string? tableName = entity.GetTableName();

                if (tableName != "Funcionarios" && tableName != "FuncionariosChefias" && tableName != "Departamentos" && tableName != "Perfis" && tableName != "PerfisJanelas" && tableName != "PerfisDepartamentos" && tableName != "Utilizadores")
                {
                    entity.SetTableName("Metrics_" + tableName);
                }
            }

            #endregion

            #region Tabelas: Funcionários, FuncionariosChefias, Departamentos
            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.Iddepartamento).HasName("IDDepartamento");

                entity.HasIndex(e => e.Identidade, "IX_Departamentos_IDEntidade").HasFillFactor(90);

                entity.Property(e => e.Iddepartamento).HasColumnName("IDDepartamento");
                entity.Property(e => e.Codigo).HasMaxLength(15);
                entity.Property(e => e.Descricao).HasMaxLength(50);
                entity.Property(e => e.EmailChefia).HasMaxLength(50);
                entity.Property(e => e.IddepartamentoPai).HasColumnName("IDDepartamentoPai");
                entity.Property(e => e.Identidade).HasColumnName("IDEntidade");
                entity.Property(e => e.Notas).HasMaxLength(500);
            });

            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.HasKey(e => e.Idfuncionario).HasName("IDFuncionario");

                entity.HasIndex(e => e.DeviceId, "IX_Funcionarios_DeviceID").HasFillFactor(90);

                entity.HasIndex(e => e.Identidade, "IX_Funcionarios_IDEntidade").HasFillFactor(90);

                entity.HasIndex(e => e.Idfuncionario, "_dta_index_Funcionarios_7_962102468__K1");

                entity.Property(e => e.Idfuncionario).HasColumnName("IDFuncionario");
                entity.Property(e => e.Assinatura).HasColumnType("image");
                entity.Property(e => e.Cartao).HasMaxLength(12);
                entity.Property(e => e.CartaoAlternativo).HasMaxLength(12);
                entity.Property(e => e.CartaoAlternativo1).HasMaxLength(15);
                entity.Property(e => e.CentroCusto).HasMaxLength(15);
                entity.Property(e => e.CodigoPostal).HasMaxLength(50);
                entity.Property(e => e.CstFullControl).HasColumnName("CST_FullControl");
                entity.Property(e => e.DataAdmissao).HasColumnType("datetime");
                entity.Property(e => e.DataDemissao).HasColumnType("datetime");
                entity.Property(e => e.DataExpiracao).HasColumnType("datetime");
                entity.Property(e => e.DataNascimento).HasColumnType("datetime");
                entity.Property(e => e.DeviceId)
                    .HasMaxLength(50)
                    .HasColumnName("DeviceID");
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.EmailEquipa).HasMaxLength(50);
                entity.Property(e => e.EstadoCivil).HasMaxLength(1);
                entity.Property(e => e.Folha).HasMaxLength(50);
                entity.Property(e => e.Fotografia).HasColumnType("image");
                entity.Property(e => e.Gdpr).HasColumnName("GDPR");
                entity.Property(e => e.HorasMensais).HasColumnType("datetime");
                entity.Property(e => e.HorasSemanais).HasColumnType("datetime");
                entity.Property(e => e.Iddepartamento).HasColumnName("IDDepartamento");
                entity.Property(e => e.Identidade).HasColumnName("IDEntidade");
                entity.Property(e => e.Idgrupo).HasColumnName("IDGrupo");
                entity.Property(e => e.Idioma).HasMaxLength(10);
                entity.Property(e => e.Idmunicipio).HasColumnName("IDMunicipio");
                entity.Property(e => e.Idperfil).HasColumnName("IDPerfil");
                entity.Property(e => e.IdperfilEquipa).HasColumnName("IDPerfilEquipa");
                entity.Property(e => e.IdperfilEquipaAcesso).HasColumnName("IDPerfilEquipaAcesso");
                entity.Property(e => e.IdperfilSuperiorHierarquico).HasColumnName("IDPerfilSuperiorHierarquico");
                entity.Property(e => e.IdpersonApp).HasColumnName("IDPersonAPP");
                entity.Property(e => e.IdplanoHorarios).HasColumnName("IDPlanoHorarios");
                entity.Property(e => e.Local).HasMaxLength(15);
                entity.Property(e => e.Localidade).HasMaxLength(50);
                entity.Property(e => e.LoginLdap)
                    .HasMaxLength(50)
                    .HasColumnName("LoginLDAP");
                entity.Property(e => e.Morada).HasMaxLength(80);
                entity.Property(e => e.Morada2).HasMaxLength(80);
                entity.Property(e => e.Nome).HasMaxLength(100);
                entity.Property(e => e.NomeAbreviado).HasMaxLength(16);
                entity.Property(e => e.Notas).HasMaxLength(500);
                entity.Property(e => e.Numero).HasMaxLength(10);
                entity.Property(e => e.OcultarBh).HasColumnName("OcultarBH");
                entity.Property(e => e.Password).HasMaxLength(50);
                entity.Property(e => e.Pin).HasMaxLength(60);
                entity.Property(e => e.Pinapk)
                    .HasMaxLength(50)
                    .HasColumnName("PINAPK");
                entity.Property(e => e.Pinapp)
                    .HasMaxLength(10)
                    .HasColumnName("PINAPP");
                entity.Property(e => e.Sexo).HasMaxLength(1);
                entity.Property(e => e.SubAlimentacao).HasMaxLength(50);
                entity.Property(e => e.Telefone).HasMaxLength(15);
                entity.Property(e => e.Telemovel).HasMaxLength(15);
                entity.Property(e => e.UltimoAcessoSmartTime).HasColumnType("datetime");
                entity.Property(e => e.ValorBase).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.ValorDiario).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.ValorHora).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.ValorHoraFds)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ValorHoraFDS");
                entity.Property(e => e.ValorPremioAssiduidade).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.ValorSubsidio).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<FuncionariosChefia>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.Iddepartamento, "IX_FuncionariosChefias_IDDepartamento").HasFillFactor(100);

                entity.HasIndex(e => e.Idfuncionario, "IX_FuncionariosChefias_IDFuncionario").HasFillFactor(100);

                entity.HasIndex(e => e.IdfuncionarioSuperior, "IX_FuncionariosChefias_IDFuncionarioSuperior").HasFillFactor(100);

                entity.Property(e => e.Iddepartamento).HasColumnName("IDDepartamento");
                entity.Property(e => e.Idfuncionario).HasColumnName("IDFuncionario");
                entity.Property(e => e.IdfuncionarioSuperior).HasColumnName("IDFuncionarioSuperior");
                entity.Property(e => e.Nivel).HasMaxLength(2);
                entity.Property(e => e.NivelAusencias).HasMaxLength(2);
                entity.Property(e => e.NivelAusenciasServico).HasMaxLength(2);
                entity.Property(e => e.NivelBh).HasColumnName("NivelBH");
                entity.Property(e => e.NivelExtras).HasMaxLength(2);
                entity.Property(e => e.NivelFerias).HasMaxLength(2);
                entity.Property(e => e.NivelFuncionariosMarcacoes).HasMaxLength(2);
            });

            modelBuilder.Entity<Perfis>(entity =>
            {
                entity.HasKey(e => e.Idperfil).HasName("IDPerfil");

                entity.Property(e => e.Idperfil).HasColumnName("IDPerfil");
                entity.Property(e => e.Descricao).HasMaxLength(50);
                entity.Property(e => e.Nome).HasMaxLength(25);
            });

            modelBuilder.Entity<PerfisDepartamento>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.Idperfil, "IX_PerfisDepartamentos_IDPerfil").HasFillFactor(90);

                entity.Property(e => e.Iddepartamento).HasColumnName("IDDepartamento");
                entity.Property(e => e.Idperfil).HasColumnName("IDPerfil");
                entity.Property(e => e.Nivel).HasMaxLength(2);
                entity.Property(e => e.NivelAusencias).HasMaxLength(2);
                entity.Property(e => e.NivelAusenciasServico).HasMaxLength(2);
                entity.Property(e => e.NivelExtras).HasMaxLength(2);
                entity.Property(e => e.NivelFerias).HasMaxLength(2);
                entity.Property(e => e.NivelFuncionariosMarcacoes).HasMaxLength(2);
            });

            modelBuilder.Entity<PerfisJanela>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => new { e.Idperfil, e.Aplicacao }, "IX_PerfisJanelas_IDPerfil_Aplicacao").HasFillFactor(90);

                entity.Property(e => e.Aplicacao).HasMaxLength(50);
                entity.Property(e => e.Idjanela).HasColumnName("IDJanela");
                entity.Property(e => e.Idperfil).HasColumnName("IDPerfil");
                entity.Property(e => e.Modulo).HasMaxLength(50);
            });

            modelBuilder.Entity<Utilizador>(entity =>
            {
                entity.HasKey(e => e.Idutilizador).HasName("IDUtilizador");

                entity.Property(e => e.Idutilizador).HasColumnName("IDUtilizador");
                entity.Property(e => e.Descricao).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.Idfuncionario).HasColumnName("IDFuncionario");
                entity.Property(e => e.Idperfil).HasColumnName("IDPerfil");
                entity.Property(e => e.Nome).HasMaxLength(100);
                entity.Property(e => e.Password).HasMaxLength(50);
                entity.Property(e => e.UltimoAcessoSmartAccess).HasColumnType("datetime");
                entity.Property(e => e.UltimoAcessoSmartTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
            #endregion

            #region Edição Manual de Relações

            //FormTemplates
            modelBuilder.Entity<FormTemplate>()
            .HasMany(ft => ft.Translations)
            .WithOne(translation => translation.FormTemplate)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FormTemplate>()
                .HasMany(ft => ft.Questions)
                .WithOne(ftq => ftq.FormTemplate)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasMany(rv => rv.Questions)
                .WithOne(q => q.Review)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Translations)
                .WithOne(translation => translation.Question)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.FormTemplate)
                .WithMany(ft => ft.Questions)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.SingleChoiceOptions)
                .WithOne(sco => sco.Question)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.RatingOptions)
                .WithOne(rto => rto.Question)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RatingOption>()
                .HasMany(rto => rto.Translations)
                .WithOne(translation => translation.RatingOption)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SingleChoiceOption>()
                .HasMany(sco => sco.Translations)
                .WithOne(translation => translation.SingleChoiceOption)
                .OnDelete(DeleteBehavior.Cascade);


            //Reviews

            modelBuilder.Entity<Review>()
                .HasMany(rv => rv.Translations)
                .WithOne(translation => translation.Review)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReviewDepartment>()
                .HasOne(rd => rd.Review)
                .WithMany(review => review.Departments)
                .HasForeignKey(rd => rd.ReviewId);

            modelBuilder.Entity<ReviewEmployee>()
               .HasOne(re => re.Review)
               .WithMany(review => review.Employees)
               .HasForeignKey(re => re.ReviewId);

            modelBuilder.Entity<Review>()
                .HasMany(review => review.Submissions)
                .WithOne(sub => sub.Review)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Submission>()
                .HasMany(sub => sub.ReviewResponses)
                .WithOne(response => response.Submission)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
               .HasOne(q => q.Review)
               .WithMany(rv => rv.Questions)
               .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Seeds
            // Semente para FormTemplate
            modelBuilder.Entity<FormTemplate>().HasData(
                new FormTemplate
                {
                    FormTemplateId = Guid.Parse("8f7f0f64-5317-4562-b3fc-2c963f66afa6"),
                    CreatedDate = DateTime.Parse("2023-11-13T10:51:27.873Z"),
                    ModifiedDate = DateTime.Parse("2023-11-13T10:51:27.873Z"),
                    CreatedByUserId = 1
                    // outras propriedades...
                }
            );

            // Semente para FormTemplateTranslation
            modelBuilder.Entity<FormTemplateTranslation>().HasData(
                new FormTemplateTranslation
                {
                    FormTemplateTranslationId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                    FormTemplateId = Guid.Parse("8f7f0f64-5317-4562-b3fc-2c963f66afa6"),
                    Language = "en",
                    Title = "Employee Satisfaction Survey",
                    Description = "A survey to measure employee satisfaction."
                }
            );

            // Semente para Question e suas traduções, RatingOptions e SingleChoiceOptions
            modelBuilder.Entity<Question>().HasData(
                new
                {
                    QuestionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa8"),
                    FormTemplateId = Guid.Parse("8f7f0f64-5317-4562-b3fc-2c963f66afa6"),
                    IsRequired = true,
                    ResponseType = "Rating",
                    Positon = 0
                },
                new
                {
                    QuestionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb1"),
                    FormTemplateId = Guid.Parse("8f7f0f64-5317-4562-b3fc-2c963f66afa6"),
                    IsRequired = true,
                    ResponseType = "SingleChoice",
                    Positon = 1
                }
            );

            modelBuilder.Entity<QuestionTranslation>().HasData(
                new
                {
                    QuestionTranslationId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afaa"),
                    QuestionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa8"),
                    Language = "en",
                    Title = "How satisfied are you with your work?",
                    Description = "Please rate your overall satisfaction with your work on a scale of 1 to 10."
                },
                new
                {
                    QuestionTranslationId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb3"),
                    QuestionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb1"),
                    Language = "en",
                    Title = "How would you rate the cafeteria food?",
                    Description = "Please select your rating for the cafeteria food."
                }
            );

            modelBuilder.Entity<RatingOption>().HasData(
                new
                {
                    RatingOptionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afab"),
                    QuestionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa8"),
                    NumericValue = 1
                    // outras propriedades...
                },
                new
                {
                    RatingOptionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afad"),
                    QuestionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa8"),
                    NumericValue = 5
                    // outras propriedades...
                },
                new
                {
                    RatingOptionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afaf"),
                    QuestionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa8"),
                    NumericValue = 10
                    // outras propriedades...
                }
            );

            modelBuilder.Entity<RatingOptionTranslation>().HasData(
                new
                {
                    RatingOptionTranslationId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afac"),
                    RatingOptionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afab"),
                    Language = "en",
                    Description = "Not Satisfied"
                },
                new
                {
                    RatingOptionTranslationId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afae"),
                    RatingOptionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afad"),
                    Language = "en",
                    Description = "Neutral"
                },
                new
                {
                    RatingOptionTranslationId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb0"),
                    RatingOptionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afaf"),
                    Language = "en",
                    Description = "Very Satisfied"
                }
            );

            modelBuilder.Entity<SingleChoiceOption>().HasData(
                new
                {
                    SingleChoiceOptionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb4"),
                    QuestionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb1")
                    // outras propriedades...
                },
                new
                {
                    SingleChoiceOptionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb6"),
                    QuestionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb1")
                    // outras propriedades...
                },
                new
                {
                    SingleChoiceOptionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb8"),
                    QuestionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb1")
                    // outras propriedades...
                }
            );

            modelBuilder.Entity<SingleChoiceOptionTranslation>().HasData(
                new
                {
                    SingleChoiceOptionTranslationId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb5"),
                    SingleChoiceOptionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb4"),
                    Language = "en",
                    Description = "Excellent"
                },
                new
                {
                    SingleChoiceOptionTranslationId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb7"),
                    SingleChoiceOptionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb6"),
                    Language = "en",
                    Description = "Good"
                },
                new
                {
                    SingleChoiceOptionTranslationId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb9"),
                    SingleChoiceOptionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb8"),
                    Language = "en",
                    Description = "Fair"
                }
            );
            #endregion
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }

}
