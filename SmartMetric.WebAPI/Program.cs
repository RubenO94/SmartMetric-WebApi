using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.Services.Adders;
using SmartMetric.Core.Services.Getters;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Core.ServicesContracts.Getters;
using SmartMetric.Infrastructure.DatabaseContext;
using SmartMetric.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IFormTemplateRepository, FormTemplatesRepository>();

builder.Services.AddScoped<IFormTemplatesGetterService, FormTemplatesGetterService>();
builder.Services.AddScoped<IFormTemplatesAdderService, FormTemplatesAdderService>();



builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add(new ConsumesAttribute("application/json"));
})
 .AddXmlSerializerFormatters();


//Enable versioning in Web API controllers
builder.Services.AddApiVersioning(config =>
{
    config.ApiVersionReader = new UrlSegmentApiVersionReader(); //Reads version number from request url at "apiVersion" constraint

    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});



builder.Services.AddControllers();
       //.AddJsonOptions(options =>
       //{
       //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
       //});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
    //options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "SmartMetric Web API", Version = "1.0" });

    //To add more versions here:

}); //generates OpenAPI specification



builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; //v1
    options.SubstituteApiVersionInUrl = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHsts();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");
    }); //creates swagger UI for testing all Web API endpoints / action methods
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
