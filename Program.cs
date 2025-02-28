using Microsoft.Extensions.Options;
using SageIntacctApi.Services;
using SageIntacctApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the Sage Intacct configuration
builder.Services.Configure<SageIntacctConfig>(builder.Configuration.GetSection("SageIntacct"));
builder.Services.AddHttpClient();
builder.Services.AddScoped<ISageIntacctService, SageIntacctService>();

builder.Logging.AddConsole(); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("/", () => "API is running!");
app.Run();