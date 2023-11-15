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

            //Dependency Injection:

            //Repositories
            services.AddScoped<IFormTemplatesRepository, FormTemplatesRepository>();
            services.AddScoped<IFormTemplateTranslationsRepository, FormTemplateTranslationsRepository>();

            //Services
            services.AddScoped<IFormTemplatesGetterService, FormTemplatesGetterService>();
            services.AddScoped<IFormTemplatesAdderService, FormTemplatesAdderService>();
            services.AddScoped<IFormTemplatesDeleterService, FormTemplatesDeleterService>();
            services.AddScoped<IFormTemplateTranslationsGetterService, FormTemplateTranslationsGetterService>();
            services.AddScoped<IFormTemplateTranslationsAdderService, FormTemplateTranslationsAdderService>();


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });


            services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });


            services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });

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


            return services;
        }
    }
}
