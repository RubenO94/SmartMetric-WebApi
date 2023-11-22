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
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using SmartMetric.WebAPI.Filters.ActionFilters;

namespace SmartMetric.WebAPI.StartupExtensions
{
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            #region Repositories

            //SmartTime
            services.AddScoped<ISmartTimeRepository, SmartTimeRepository>();

            services.AddScoped<IFormTemplatesRepository, FormTemplatesRepository>();
            services.AddScoped<IFormTemplateTranslationsRepository, FormTemplateTranslationsRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionTranslationsRepository, QuestionTranslationsRepository>();
            services.AddScoped<IRatingOptionRepository, RatingOptionRepository>();
            services.AddScoped<IRatingOptionTranslationsRepository, RatingOptionTranslationsRepository>();
            services.AddScoped<ISingleChoiceOptionRepository, SingleChoiceOptionRepository>();
            services.AddScoped<ISingleChoiceOptionTranslationsRepository, SingleChoiceOptionTranslationsRepository>();
            services.AddScoped<ISmartTimeRepository, SmartTimeRepository>();

            #endregion

            #region Services

            //SmartTime
            services.AddScoped<ISmartTimeService, SmartTimeService>();

            //JWT
            services.AddTransient<IJwtService, JwtService>();

            //Scoped Services:
            services.AddScoped<IFormTemplatesGetterService, FormTemplatesGetterService>();
            services.AddScoped<IFormTemplatesAdderService, FormTemplatesAdderService>();
            services.AddScoped<IFormTemplatesDeleterService, FormTemplatesDeleterService>();
            services.AddScoped<IFormTemplateTranslationsGetterService, FormTemplateTranslationsGetterService>();
            services.AddScoped<IFormTemplateTranslationsAdderService, FormTemplateTranslationsAdderService>();
            services.AddScoped<IFormTemplateTranslationsDeleterService, FormTemplateTranslationDeleterService>();
            services.AddScoped<IQuestionAdderService, QuestionAdderService>();
            services.AddScoped<IQuestionGetterService, QuestionGetterService>();
            services.AddScoped<IQuestionTranslationAdderService, QuestionTranslationAdderService>();
            services.AddScoped<IQuestionTranslationDeleterService, QuestionTranslationDeleterService>();
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
                options.Filters.Add<TokenValidationActionFilter>();

                //Authorization policy
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                options.Filters.Add(new AuthorizeFilter(policy));
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

            #region CORS

            services.AddCors(options => {
                options.AddDefaultPolicy(policyBuilder =>
                {
                    policyBuilder
                    .WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>()!)
                    .WithHeaders("Authorization", "origin", "accept", "content-type")
                    .WithMethods("GET", "POST", "PUT", "DELETE")
                    ;
                });
            });

            #endregion

            #region JWT

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = configuration["Jwt:Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                    };
                });

            services.AddAuthorization();

            #endregion

            return services;
        }
    }
}
