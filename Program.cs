using SageIntacctApi.Services;
using SageIntacctApi.Models;
using Microsoft.Extensions.Logging;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

//  Sage Intacct configuration
builder.Services.Configure<SageIntacctConfig>(builder.Configuration.GetSection("SageIntacct"));
builder.Services.AddHttpClient();
builder.Services.AddScoped<ISageIntacctService, SageIntacctService>();


builder.Logging.AddConsole();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseRouting();
    app.MapControllers();

    app.Use(async (context, next) =>
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");
        await next();
        logger.LogInformation($"Response: {context.Response.StatusCode}");
    });

}

app.UseHttpsRedirection();
app.MapGet("/", () => "API is running!");

app.Run();
