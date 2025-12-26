using PokemonApi.Models;
using PokemonApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DBSettings>(builder.Configuration.GetSection("DBSettings"));
builder.Services.AddScoped<PokemonService>();

// Controllers
builder.Services.AddControllers();

// Built-in OpenAPI document
builder.Services.AddOpenApi();

var app = builder.Build();

// OpenAPI + Swagger UI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Pokemon API v1");
});

// Map attribute-routed controllers
app.MapControllers();

app.Run();
