﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartMetric.Infrastructure.DatabaseContext;

#nullable disable

namespace SmartMetric.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.FormTemplate", b =>
                {
                    b.Property<Guid>("FormTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
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

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.FormTemplateQuestion", b =>
                {
                    b.Property<Guid>("FormTemplateQuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FormTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FormTemplateQuestionId");

                    b.HasIndex("FormTemplateId");

                    b.HasIndex("QuestionId");

                    b.ToTable("FormTemplateQuestions");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.FormTemplateTranslation", b =>
                {
                    b.Property<Guid>("FormTemplateTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<Guid?>("FormTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Language")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("FormTemplateTranslationId");

                    b.HasIndex("FormTemplateId");

                    b.ToTable("FormTemplateTranslations");

                    b.HasData(
                        new
                        {
                            FormTemplateTranslationId = new Guid("5e46bb2d-7801-4c38-9b5d-2e678f0b60c6"),
                            Description = "Descrição do Modelo de Formulário 1",
                            FormTemplateId = new Guid("8c57f6d6-2a90-4f84-b32c-5c57d86e5c20"),
                            Language = "PT",
                            Title = "Título do Modelo de Formulário 1"
                        },
                        new
                        {
                            FormTemplateTranslationId = new Guid("0c5a48d3-e745-4e2b-b6e9-c93ec313072f"),
                            Description = "Form Template Description 1 (English)",
                            FormTemplateId = new Guid("8c57f6d6-2a90-4f84-b32c-5c57d86e5c20"),
                            Language = "EN",
                            Title = "Form Template Title 1 (English)"
                        },
                        new
                        {
                            FormTemplateTranslationId = new Guid("b9e19ca5-df90-46b3-94ed-f5457a9f3e27"),
                            Description = "Descrição do Modelo de Formulário 2",
                            FormTemplateId = new Guid("e4ea5e10-9b3e-4a45-8b3f-7cf7417e556f"),
                            Language = "PT",
                            Title = "Título do Modelo de Formulário 2"
                        },
                        new
                        {
                            FormTemplateTranslationId = new Guid("67404860-251f-4c6e-ba5c-2a848b2e7f85"),
                            Description = "Form Template Description 2 (English)",
                            FormTemplateId = new Guid("e4ea5e10-9b3e-4a45-8b3f-7cf7417e556f"),
                            Language = "EN",
                            Title = "Form Template Title 2 (English)"
                        });
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.Question", b =>
                {
                    b.Property<Guid>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<int?>("Position")
                        .HasColumnType("int");

                    b.Property<string>("ResponseType")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("QuestionId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.QuestionTranslation", b =>
                {
                    b.Property<Guid>("QuestionTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Language")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("QuestionTranslationId");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionTranslations");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.RatingOption", b =>
                {
                    b.Property<Guid>("RatingOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumericValue")
                        .HasColumnType("int");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RatingOptionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("RatingOptions");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.RatingOptionTranslation", b =>
                {
                    b.Property<Guid>("RatingOptionTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Language")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<Guid>("RatingOptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RatingOptionTranslationId");

                    b.HasIndex("RatingOptionId");

                    b.ToTable("RatingOptionTranslations");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.Review", b =>
                {
                    b.Property<Guid>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("FormTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReviewStatus")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ReviewType")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubjectType")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ReviewId");

                    b.HasIndex("FormTemplateId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.ReviewQuestion", b =>
                {
                    b.Property<Guid>("ReviewQuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("QuestionId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ReviewId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReviewQuestionId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("ReviewId");

                    b.ToTable("ReviewsQuestions");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.ReviewResponse", b =>
                {
                    b.Property<Guid>("ReviewResponseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("RatingValue")
                        .HasColumnType("int");

                    b.Property<Guid?>("ReviewQuestionId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SingleChoiceOptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubmissionId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TextResponse")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("ReviewResponseId");

                    b.HasIndex("ReviewQuestionId");

                    b.HasIndex("SingleChoiceOptionId");

                    b.HasIndex("SubmissionId");

                    b.ToTable("ReviewResponses");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.SingleChoiceOption", b =>
                {
                    b.Property<Guid>("SingleChoiceOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SingleChoiceOptionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("SingleChoiceOptions");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.SingleChoiceOptionTranslation", b =>
                {
                    b.Property<Guid>("SingleChoiceOptionTranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Language")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid?>("SingleChoiceOptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SingleChoiceOptionTranslationId");

                    b.HasIndex("SingleChoiceOptionId");

                    b.ToTable("SingleChoiceOptionTranslations");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.Submission", b =>
                {
                    b.Property<Guid>("SubmissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("EvaluatedEmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("EvaluatorEmployeeId")
                        .HasColumnType("int");

                    b.Property<Guid?>("ReviewId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SubmissionId");

                    b.HasIndex("ReviewId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.FormTemplateQuestion", b =>
                {
                    b.HasOne("SmartMetric.Core.Domain.Entities.FormTemplate", "FormTemplate")
                        .WithMany("FormTemplateQuestions")
                        .HasForeignKey("FormTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartMetric.Core.Domain.Entities.Question", "Question")
                        .WithMany("FormTemplateQuestions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormTemplate");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.FormTemplateTranslation", b =>
                {
                    b.HasOne("SmartMetric.Core.Domain.Entities.FormTemplate", "FormTemplate")
                        .WithMany("Translations")
                        .HasForeignKey("FormTemplateId");

                    b.Navigation("FormTemplate");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.QuestionTranslation", b =>
                {
                    b.HasOne("SmartMetric.Core.Domain.Entities.Question", "Question")
                        .WithMany("Translations")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.RatingOption", b =>
                {
                    b.HasOne("SmartMetric.Core.Domain.Entities.Question", "Question")
                        .WithMany("RatingOptions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.RatingOptionTranslation", b =>
                {
                    b.HasOne("SmartMetric.Core.Domain.Entities.RatingOption", "RatingOption")
                        .WithMany("Translations")
                        .HasForeignKey("RatingOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RatingOption");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.Review", b =>
                {
                    b.HasOne("SmartMetric.Core.Domain.Entities.FormTemplate", "FormTemplate")
                        .WithMany()
                        .HasForeignKey("FormTemplateId");

                    b.Navigation("FormTemplate");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.ReviewQuestion", b =>
                {
                    b.HasOne("SmartMetric.Core.Domain.Entities.Question", "Question")
                        .WithMany("ReviewQuestions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartMetric.Core.Domain.Entities.Review", "Review")
                        .WithMany()
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Review");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.ReviewResponse", b =>
                {
                    b.HasOne("SmartMetric.Core.Domain.Entities.ReviewQuestion", "ReviewQuestion")
                        .WithMany()
                        .HasForeignKey("ReviewQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartMetric.Core.Domain.Entities.SingleChoiceOption", "SingleChoiceOption")
                        .WithMany()
                        .HasForeignKey("SingleChoiceOptionId");

                    b.HasOne("SmartMetric.Core.Domain.Entities.Submission", "Submission")
                        .WithMany("ReviewResponses")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReviewQuestion");

                    b.Navigation("SingleChoiceOption");

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.SingleChoiceOption", b =>
                {
                    b.HasOne("SmartMetric.Core.Domain.Entities.Question", "Question")
                        .WithMany("SingleChoiceOptions")
                        .HasForeignKey("QuestionId");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.SingleChoiceOptionTranslation", b =>
                {
                    b.HasOne("SmartMetric.Core.Domain.Entities.SingleChoiceOption", "SingleChoiceOption")
                        .WithMany("Translations")
                        .HasForeignKey("SingleChoiceOptionId");

                    b.Navigation("SingleChoiceOption");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.Submission", b =>
                {
                    b.HasOne("SmartMetric.Core.Domain.Entities.Review", "Review")
                        .WithMany("Submissions")
                        .HasForeignKey("ReviewId");

                    b.Navigation("Review");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.FormTemplate", b =>
                {
                    b.Navigation("FormTemplateQuestions");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.Question", b =>
                {
                    b.Navigation("FormTemplateQuestions");

                    b.Navigation("RatingOptions");

                    b.Navigation("ReviewQuestions");

                    b.Navigation("SingleChoiceOptions");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.RatingOption", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.Review", b =>
                {
                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.SingleChoiceOption", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("SmartMetric.Core.Domain.Entities.Submission", b =>
                {
                    b.Navigation("ReviewResponses");
                });
#pragma warning restore 612, 618
        }
    }
}
