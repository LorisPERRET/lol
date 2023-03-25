using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Model;
using StubLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Manager choice
builder.Services.AddSingleton<IDataManager, StubData>();
builder.Services.AddSingleton<ILogger, Logger<AnyType>>();

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//Doc Swagger Versionning
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "League of Legends API", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "League of Legends API", Version = "v2" });
});

// Versionning
builder.Services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new ApiVersion(2, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
    o.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();


//Doc Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("./v1/swagger.json", "League of Legends API V1");
    options.SwaggerEndpoint("./v2/swagger.json", "League of Legends API V2");
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
