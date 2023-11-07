﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartMetric.Infrastructure.DatabaseContext;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231107160052_AddSeedToFormTemplatesTranslations")]
    partial class AddSeedToFormTemplatesTranslations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmartMetric.Core.Entities.FormTemplate", b =>
                {
                    b.Property<Guid>("FormTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("FormTemplateId");

                    b.ToTable("FormTemplates");

                    b.HasData(
                        new
                        {
                            FormTemplateId = new Guid("8c57f6d6-2a90-4f84-b32c-5c57d86e5c20"),
                            CreatedByUserId = 1,
                            CreatedDate = new DateTime(2023, 11, 6, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            FormTemplateId = new Guid("e4ea5e10-9b3e-4a45-8b3f-7cf7417e556f"),
                            CreatedByUserId = 2,
                            CreatedDate = new DateTime(2023, 11, 6, 14, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.FormTemplateTranslation", b =>
                {
                    b.Property<Guid>("FormTemplateTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FormTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Language")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FormTemplateTranslationId");

                    b.HasIndex("FormTemplateId");

                    b.ToTable("FormTemplateTranslations");

                    b.HasData(
                        new
                        {
                            FormTemplateTranslationId = new Guid("5e46bb2d-7801-4c38-9b5d-2e678f0b60c6"),
                            Description = "Descrição do Modelo de Formulário 1",
                            FormTemplateId = new Guid("8c57f6d6-2a90-4f84-b32c-5c57d86e5c20"),
                            Language = 2,
                            Title = "Título do Modelo de Formulário 1"
                        },
                        new
                        {
                            FormTemplateTranslationId = new Guid("0c5a48d3-e745-4e2b-b6e9-c93ec313072f"),
                            Description = "Form Template Description 1 (English)",
                            FormTemplateId = new Guid("8c57f6d6-2a90-4f84-b32c-5c57d86e5c20"),
                            Language = 1,
                            Title = "Form Template Title 1 (English)"
                        },
                        new
                        {
                            FormTemplateTranslationId = new Guid("b9e19ca5-df90-46b3-94ed-f5457a9f3e27"),
                            Description = "Descrição do Modelo de Formulário 2",
                            FormTemplateId = new Guid("e4ea5e10-9b3e-4a45-8b3f-7cf7417e556f"),
                            Language = 2,
                            Title = "Título do Modelo de Formulário 2"
                        },
                        new
                        {
                            FormTemplateTranslationId = new Guid("67404860-251f-4c6e-ba5c-2a848b2e7f85"),
                            Description = "Form Template Description 2 (English)",
                            FormTemplateId = new Guid("e4ea5e10-9b3e-4a45-8b3f-7cf7417e556f"),
                            Language = 1,
                            Title = "Form Template Title 2 (English)"
                        });
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.Question", b =>
                {
                    b.Property<Guid>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FormTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<Guid?>("RatingTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ResponseType")
                        .HasColumnType("int");

                    b.Property<Guid?>("SingleChoiceTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("QuestionId");

                    b.HasIndex("FormTemplateId");

                    b.HasIndex("RatingTemplateId");

                    b.HasIndex("SingleChoiceTemplateId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.QuestionTranslation", b =>
                {
                    b.Property<Guid>("QuestionTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Language")
                        .HasColumnType("int");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionTranslationId");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionTranslations");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.RatingOption", b =>
                {
                    b.Property<Guid>("RatingOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumericValue")
                        .HasColumnType("int");

                    b.Property<Guid>("RatingTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RatingOptionId");

                    b.HasIndex("RatingTemplateId");

                    b.ToTable("RatingOptions");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.RatingOptionTranslation", b =>
                {
                    b.Property<Guid>("RatingOptionTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Language")
                        .HasColumnType("int");

                    b.Property<Guid?>("RatingOptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RatingTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RatingOptionTranslationId");

                    b.HasIndex("RatingOptionId");

                    b.HasIndex("RatingTemplateId");

                    b.ToTable("RatingOptionTranslations");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.RatingTemplate", b =>
                {
                    b.Property<Guid>("RatingTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("RatingTemplateId");

                    b.ToTable("RatingTemplates");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.RatingTemplateTranslation", b =>
                {
                    b.Property<Guid>("RatingTemplateTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Language")
                        .HasColumnType("int");

                    b.Property<Guid>("RatingTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RatingTemplateTranslationId");

                    b.HasIndex("RatingTemplateId");

                    b.ToTable("RatingTemplateTranslations");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.Review", b =>
                {
                    b.Property<Guid>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EvaluatorEmployeeId")
                        .HasColumnType("int");

                    b.Property<Guid>("FormTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubjectType")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("FormTemplateId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.ReviewResponse", b =>
                {
                    b.Property<Guid>("ReviewResponseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("RatingValue")
                        .HasColumnType("int");

                    b.Property<Guid?>("SingleChoiceOptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubmissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TextResponse")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReviewResponseId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("SingleChoiceOptionId");

                    b.HasIndex("SubmissionId");

                    b.ToTable("ReviewResponses");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.SingleChoiceOption", b =>
                {
                    b.Property<Guid>("SingleChoiceOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SingleChoiceTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SingleChoiceOptionId");

                    b.HasIndex("SingleChoiceTemplateId");

                    b.ToTable("SingleChoiceOptions");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.SingleChoiceOptionTranslation", b =>
                {
                    b.Property<Guid>("SingleChoiceOptionTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Language")
                        .HasColumnType("int");

                    b.Property<Guid?>("SingleChoiceOptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SingleChoiceTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SingleChoiceOptionTranslationId");

                    b.HasIndex("SingleChoiceOptionId");

                    b.HasIndex("SingleChoiceTemplateId");

                    b.ToTable("SingleChoiceOptionTranslations");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.SingleChoiceTemplate", b =>
                {
                    b.Property<Guid>("SingleChoiceTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SingleChoiceTemplateId");

                    b.ToTable("SingleChoiceTemplates");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.SingleChoiceTemplateTranslation", b =>
                {
                    b.Property<Guid>("SingleChoiceTemplateTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Language")
                        .HasColumnType("int");

                    b.Property<Guid>("SingleChoiceTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SingleChoiceTemplateTranslationId");

                    b.HasIndex("SingleChoiceTemplateId");

                    b.ToTable("SingleChoiceTemplateTranslations");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.Submission", b =>
                {
                    b.Property<Guid>("SubmissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReviewId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SubmissionId");

                    b.HasIndex("ReviewId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.FormTemplateTranslation", b =>
                {
                    b.HasOne("SmartMetric.Core.Entities.FormTemplate", "FormTemplate")
                        .WithMany("Translations")
                        .HasForeignKey("FormTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormTemplate");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.Question", b =>
                {
                    b.HasOne("SmartMetric.Core.Entities.FormTemplate", "FormTemplate")
                        .WithMany("Questions")
                        .HasForeignKey("FormTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartMetric.Core.Entities.RatingTemplate", "RatingTemplate")
                        .WithMany()
                        .HasForeignKey("RatingTemplateId");

                    b.HasOne("SmartMetric.Core.Entities.SingleChoiceTemplate", "SingleChoiceTemplate")
                        .WithMany()
                        .HasForeignKey("SingleChoiceTemplateId");

                    b.Navigation("FormTemplate");

                    b.Navigation("RatingTemplate");

                    b.Navigation("SingleChoiceTemplate");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.QuestionTranslation", b =>
                {
                    b.HasOne("SmartMetric.Core.Entities.Question", "Question")
                        .WithMany("Translations")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.RatingOption", b =>
                {
                    b.HasOne("SmartMetric.Core.Entities.RatingTemplate", "RatingTemplate")
                        .WithMany("RatingOptions")
                        .HasForeignKey("RatingTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RatingTemplate");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.RatingOptionTranslation", b =>
                {
                    b.HasOne("SmartMetric.Core.Entities.RatingOption", null)
                        .WithMany("Translations")
                        .HasForeignKey("RatingOptionId");

                    b.HasOne("SmartMetric.Core.Entities.RatingTemplate", "RatingTemplate")
                        .WithMany()
                        .HasForeignKey("RatingTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RatingTemplate");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.RatingTemplateTranslation", b =>
                {
                    b.HasOne("SmartMetric.Core.Entities.RatingTemplate", "RatingTemplate")
                        .WithMany("Translations")
                        .HasForeignKey("RatingTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RatingTemplate");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.Review", b =>
                {
                    b.HasOne("SmartMetric.Core.Entities.FormTemplate", "FormTemplate")
                        .WithMany()
                        .HasForeignKey("FormTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormTemplate");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.ReviewResponse", b =>
                {
                    b.HasOne("SmartMetric.Core.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartMetric.Core.Entities.SingleChoiceOption", "SingleChoiceOption")
                        .WithMany()
                        .HasForeignKey("SingleChoiceOptionId");

                    b.HasOne("SmartMetric.Core.Entities.Submission", "Submission")
                        .WithMany("ReviewResponses")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("SingleChoiceOption");

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.SingleChoiceOption", b =>
                {
                    b.HasOne("SmartMetric.Core.Entities.SingleChoiceTemplate", "SingleChoiceTemplate")
                        .WithMany("SingleChoiceOptions")
                        .HasForeignKey("SingleChoiceTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SingleChoiceTemplate");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.SingleChoiceOptionTranslation", b =>
                {
                    b.HasOne("SmartMetric.Core.Entities.SingleChoiceOption", null)
                        .WithMany("Translations")
                        .HasForeignKey("SingleChoiceOptionId");

                    b.HasOne("SmartMetric.Core.Entities.SingleChoiceTemplate", "SingleChoiceTemplate")
                        .WithMany()
                        .HasForeignKey("SingleChoiceTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SingleChoiceTemplate");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.SingleChoiceTemplateTranslation", b =>
                {
                    b.HasOne("SmartMetric.Core.Entities.SingleChoiceTemplate", "SingleChoiceTemplate")
                        .WithMany("Translations")
                        .HasForeignKey("SingleChoiceTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SingleChoiceTemplate");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.Submission", b =>
                {
                    b.HasOne("SmartMetric.Core.Entities.Review", "Review")
                        .WithMany("Submissions")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.FormTemplate", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.Question", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.RatingOption", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.RatingTemplate", b =>
                {
                    b.Navigation("RatingOptions");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.Review", b =>
                {
                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.SingleChoiceOption", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.SingleChoiceTemplate", b =>
                {
                    b.Navigation("SingleChoiceOptions");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("SmartMetric.Core.Entities.Submission", b =>
                {
                    b.Navigation("ReviewResponses");
                });
#pragma warning restore 612, 618
        }
    }
}
