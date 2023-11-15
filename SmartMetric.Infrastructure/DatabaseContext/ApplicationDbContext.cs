using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

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
        public DbSet<Submission> Submissions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Edição de Relações Manualemente:
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
                .HasOne(q => q.Review)
                .WithMany(rv => rv.Questions)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.SingleChoiceOptions)
                .WithOne(sco => sco.Question)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.RatingOptions)
                .WithOne(rto  => rto.Question)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RatingOption>()
                .HasMany(rto => rto.Translations)
                .WithOne(translation => translation.RatingOption)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SingleChoiceOption>()
                .HasMany(sco => sco.Translations)
                .WithOne( translation => translation.SingleChoiceOption)
                .OnDelete(DeleteBehavior.Cascade);




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
                    ResponseType = "Rating"
                    // outras propriedades...
                },
                new
                {
                    QuestionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb1"),
                    FormTemplateId = Guid.Parse("8f7f0f64-5317-4562-b3fc-2c963f66afa6"),
                    IsRequired = true,
                    ResponseType = "SingleChoice"
                    // outras propriedades...
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

            // Semente para FormTemplateQuestion
            //modelBuilder.Entity<FormTemplateQuestion>().HasData(
            //    new FormTemplateQuestion
            //    {
            //        FormTemplateId = Guid.Parse("8f7f0f64-5317-4562-b3fc-2c963f66afa6"),
            //        QuestionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa8")
            //    },
            //    new FormTemplateQuestion
            //    {
            //        FormTemplateId = Guid.Parse("8f7f0f64-5317-4562-b3fc-2c963f66afa6"),
            //        QuestionId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb1")
            //    }
            //);
        }



    }

}
