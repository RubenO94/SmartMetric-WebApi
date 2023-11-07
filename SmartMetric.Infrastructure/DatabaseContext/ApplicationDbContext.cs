using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using SmartMetric.Core.Entities;
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
        public DbSet<RatingTemplate> RatingTemplates { get; set; }
        public DbSet<RatingTemplateTranslation> RatingTemplateTranslations { get; set; }
        public DbSet<RatingOption> RatingOptions { get; set; }
        public DbSet<RatingOptionTranslation> RatingOptionTranslations { get; set; }
        public DbSet<SingleChoiceTemplate> SingleChoiceTemplates { get; set; }
        public DbSet<SingleChoiceTemplateTranslation> SingleChoiceTemplateTranslations { get; set; }
        public DbSet<SingleChoiceOption> SingleChoiceOptions { get; set; }
        public DbSet<SingleChoiceOptionTranslation> SingleChoiceOptionTranslations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewResponse> ReviewResponses { get; set; }
        public DbSet<Submission> Submissions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Seed to FormTemplateTranslations
            string formTemplateTranslationsJson = System.IO.File.ReadAllText("formTemplateTranslations.json");

            List<FormTemplateTranslation>? formTemplateTranslations = System.Text.Json.JsonSerializer.Deserialize<List<FormTemplateTranslation>>(formTemplateTranslationsJson);

            if(formTemplateTranslations != null)
            foreach (var translation in formTemplateTranslations)
            {
                modelBuilder.Entity<FormTemplateTranslation>().HasData(translation);
            }

            //Seed to FormTemplates
            string formTemplatesJson = System.IO.File.ReadAllText("formTemplates.json");

            List<FormTemplate>? formTemplates = System.Text.Json.JsonSerializer.Deserialize<List<FormTemplate>>(formTemplatesJson);

            if (formTemplates != null)
                foreach (var template in formTemplates)
                {
                    //if (template.Questions != null)
                    //    foreach (var question in template.Questions)
                    //    {
                    //        if (question.Translations != null)
                    //            foreach (var translation in question.Translations)
                    //            {
                    //                modelBuilder.Entity<QuestionTranslation>().HasData(translation);
                    //            }

                    //        modelBuilder.Entity<Question>().HasData(question);
                    //    }

                    if (template.Translations != null)
                        foreach (var translation in template.Translations)
                        {
                            modelBuilder.Entity<FormTemplateTranslation>().HasData(translation);
                        }

                    modelBuilder.Entity<FormTemplate>().HasData(template);
                }
        }

    }

}
