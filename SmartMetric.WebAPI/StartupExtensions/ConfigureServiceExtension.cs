using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.Services.Adders;
using SmartMetric.Core.Services.Getters;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Getters;
using SmartMetric.Infrastructure.DatabaseContext;
using SmartMetric.Infrastructure.Repositories;
using System.Text.Json.Serialization;
using SmartMetric.Core.ServicesContracts.Deleters;
using SmartMetric.Core.Services.Deleters;

namespace SmartMetric.WebAPI.StartupExtensions
{
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            #region Repositories

            services.AddScoped<IFormTemplatesRepository, FormTemplatesRepository>();
            services.AddScoped<IFormTemplateTranslationsRepository, FormTemplateTranslationsRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IRatingOptionRepository, RatingOptionRepository>();
            services.AddScoped<IRatingOptionTranslationsRepository, RatingOptionTranslationsRepository>();
            services.AddScoped<ISingleChoiceOptionRepository, SingleChoiceOptionRepository>();
            services.AddScoped<ISingleChoiceOptionTranslationsRepository, SingleChoiceOptionTranslationsRepository>();

            #endregion

            #region Services

            services.AddScoped<IFormTemplatesGetterService, FormTemplatesGetterService>();
            services.AddScoped<IFormTemplatesAdderService, FormTemplatesAdderService>();
            services.AddScoped<IFormTemplatesDeleterService, FormTemplatesDeleterService>();
            services.AddScoped<IFormTemplateTranslationsGetterService, FormTemplateTranslationsGetterService>();
            services.AddScoped<IFormTemplateTranslationsAdderService, FormTemplateTranslationsAdderService>();
            services.AddScoped<IFormTemplateTranslationsDeleterService, FormTemplateTranslationDeleterService>();
            services.AddScoped<IQuestionAdderService, QuestionAdderService>();
            services.AddScoped<IQuestionGetterService, QuestionGetterService>();
            services.AddScoped<IRatingOptionAdderService, RatingOptionAdderService>();
            services.AddScoped<IRatingOptionGetterService, RatingOptionGetterService>();
            services.AddScoped<IRatingOptionDeleterService, RatingOptionDeleterService>();
            services.AddScoped<IRatingOptionTranslationAdderService, RatingOptionTranslationsAdderService>();
            services.AddScoped<IRatingOptionTranslationDeleterService, RatingOptionTranslationDeleterService>();
            services.AddScoped<ISingleChoiceOptionsAdderService, SingleChoiceOptionAdderService>();
            services.AddScoped<ISingleChoiceOptionGetterService, SingleChoiceOptionGetterService>();
            services.AddScoped<ISingleChoiceOptionDeleterService, SingleChoiceOptionDeleterService>();
            services.AddScoped<ISingleChoiceOptionTranslationsAdderService, SingleChoiceOptionTranslationsAdderService>();
            services.AddScoped<ISingleChoiceOptionTranslationDeleterService, SingleChoiceOptionTranslationDeleterService>();

            #endregion

            #region EntityFrameWork Configurations

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });

            #endregion

            #region Loggers

            services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });

            #endregion

            #region Controllers

            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));
            })
             .AddXmlSerializerFormatters()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                 options.JsonSerializerOptions.DefaultIgnoreCondition =
                     JsonIgnoreCondition.WhenWritingNull;
             });

            #endregion

            #region API Versioning

            //Enable versioning in Web API controllers
            services.AddApiVersioning(config =>
            {
                config.ApiVersionReader = new UrlSegmentApiVersionReader(); //Reads version number from request url at "apiVersion" constraint

                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
                //options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "SmartMetric Web API", Version = "1.0" });

                //To add more versions here:

            }); //generates OpenAPI specification



            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV"; //v1
                options.SubstituteApiVersionInUrl = true;
            });

            #endregion

            return services;
        }
    }
}
