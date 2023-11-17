using SmartMetric.WebAPI.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseExceptionHandler("/error");

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
else
{
    //Exception Handling
    app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.ConfigureExceptionHandler());
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
