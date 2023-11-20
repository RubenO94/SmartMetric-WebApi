using Microsoft.AspNetCore.Diagnostics;
using SmartMetric.Core.Exceptions;
using SmartMetric.WebAPI.StartupExtensions;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseExceptionHandler(c => c.Run(async context =>
//{

//    var exception = context.Features
//        .Get<IExceptionHandlerPathFeature>()?
//        .Error;

//    HttpStatusCode code;

//    if (exception is HttpStatusException errorResponse)
//    {
//        code = errorResponse.Status;
//    }
//    else
//    {
//        code = HttpStatusCode.InternalServerError;
//    }

//    var response = new
//    {
//        code = (int)code,
//        error = exception?.Message,
//    };
//    context.Response.StatusCode = (int)code;
//    await context.Response.WriteAsJsonAsync(response);
//}));
app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.ConfigureExceptionHandler());

app.UseHsts();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
    }); //creates swagger UI for testing all Web API endpoints / action methods
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
