using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Infrastructure.DatabaseContext;
using SmartMetric.Infrastructure.Repositories;
using System.Text.Json.Serialization;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using SmartMetric.WebAPI.Filters.ActionFilters;
using SmartMetric.WebAPI.Filters.ExceptionFilter;
using SmartMetric.WebAPI.Filters.ActionFilter;
using SmartMetric.Core.ServicesContracts.FormTemplates;
using SmartMetric.Core.Services.FormTemplates;
using SmartMetric.Core.ServicesContracts.FormTemplateTranslations;
using SmartMetric.Core.Services.FormTemplateTranslations;
using SmartMetric.Core.ServicesContracts.Questions;
using SmartMetric.Core.Services.Questions;
using SmartMetric.Core.ServicesContracts.QuestionTranslations;
using SmartMetric.Core.Services.QuestionTranslations;
using SmartMetric.Core.ServicesContracts.RatingOptions;
using SmartMetric.Core.Services.RatingOptions;
using SmartMetric.Core.ServicesContracts.RatingOptionTranslations;
using SmartMetric.Core.Services.RatingOptionTranslations;
using SmartMetric.Core.ServicesContracts.SingleChoiceOptions;
using SmartMetric.Core.Services.SingleChoiceOptions;
using SmartMetric.Core.ServicesContracts.SingleChoiceOptionTranslations;
using SmartMetric.Core.ServicesContracts.Reviews;
using SmartMetric.Core.Services.Reviews;
using SmartMetric.Core.Services.SingleChoiceOptionTranslations;
using Microsoft.OpenApi.Models;
using SmartMetric.Core.ServicesContracts.Submissions;
using SmartMetric.Core.Services.Submissions;
using SmartMetric.Core.ServicesContracts.Submission;

namespace SmartMetric.WebAPI.StartupExtensions
{
    /// <summary>
    /// Métodos de extensão para configurar serviços da  WEB API SmartMetric.
    /// </summary>
    public static class ConfigureServiceExtension
    {
        /// <summary>
        /// Configura os serviços para a WEB API SmartMetric.
        /// </summary>
        /// <param name="services">A coleção de descritores de serviços.</param>
        /// <param name="configuration">As configurações de configuração.</param>
        /// <returns>A coleção atualizada de descritores de serviços.</returns>
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            #region Repositories
            //SmartTime
            services.AddScoped<ISmartTimeRepository, SmartTimeRepository>();

            //Metrics
            //services.AddScoped<IBaseRepository, BaseRepository>();

            services.AddScoped<IFormTemplateRepository, FormTemplateRepository>();
            services.AddScoped<IFormTemplateTranslationRepository, FormTemplateTranslationRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionTranslationRepository, QuestionTranslationRepository>();
            services.AddScoped<IRatingOptionRepository, RatingOptionRepository>();
            services.AddScoped<IRatingOptionTranslationsRepository, RatingOptionTranslationRepository>();
            services.AddScoped<ISingleChoiceOptionRepository, SingleChoiceOptionRepository>();
            services.AddScoped<ISingleChoiceOptionTranslationRepository, SingleChoiceOptionTranslationRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<ISubmissionRepository, SubmissionRepository>();

            #endregion

            #region Services

            //SmartTime
            services.AddScoped<ISmartTimeService, SmartTimeService>();

            //JWT
            services.AddTransient<IJwtService, JwtService>();

            //Metrics Services:
            services.AddScoped<IFormTemplateGetterService, FormTemplatesGetterService>();
            services.AddScoped<IFormTemplateAdderService, FormTemplatesAdderService>();
            services.AddScoped<IFormTemplateDeleterService, FormTemplatesDeleterService>();
            services.AddScoped<IFormTemplateUpdaterService, FormTemplateUpdaterService>();

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

            services.AddScoped<ISingleChoiceOptionAdderService, SingleChoiceOptionAdderService>();
            services.AddScoped<ISingleChoiceOptionGetterService, SingleChoiceOptionGetterService>();
            services.AddScoped<ISingleChoiceOptionDeleterService, SingleChoiceOptionDeleterService>();

            services.AddScoped<ISingleChoiceOptionTranslationsAdderService, SingleChoiceOptionTranslationsAdderService>();
            services.AddScoped<ISingleChoiceOptionTranslationDeleterService, SingleChoiceOptionTranslationDeleterService>();

            services.AddScoped<IReviewAdderService, ReviewAdderService>();
            services.AddScoped<IReviewGetterService, ReviewGetterService>();
            services.AddScoped<IReviewDeleterService, ReviewDeleterService>();
            services.AddScoped<IReviewUpdaterService, ReviewUpdaterService>();

            services.AddScoped<ISubmissionAdderService, SubmissionAdderService>();
            services.AddScoped<ISubmissionGetterService, SubmissionGetterService>();
            services.AddScoped<ISubmissionUpdaterService, SubmissionUpdaterService>();
            services.AddScoped<ISubmissionDeleterService, SubmissionDeleterService>();

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
                options.Filters.Add<TokenValidationActionFilter>();
                options.Filters.Add(typeof(PermissionFilter));
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));
                options.Filters.Add(typeof(RequestValidationAttribute));
                options.Filters.Add(typeof(ValidationErrorHandlingAttribute));

                //Authorization policy
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            })
             .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; })
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
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));

                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "SmartMetric Web API", Version = "1.0" });
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "SmartMetric Web API", Version = "2.0" });

                //To add more versions here:

            }); //generates OpenAPI specification



            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV"; //v1
                options.SubstituteApiVersionInUrl = true;
            });

            #endregion

            #region CORS

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policyBuilder =>
                {
                    policyBuilder
                    .WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>()!)
                    .WithHeaders("Authorization", "origin", "accept", "content-type")
                    .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH")
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
