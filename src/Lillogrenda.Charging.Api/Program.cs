global using FastEndpoints;
using FastEndpoints.Swagger;
using Lillogrenda.Charging.Api.Extensions;
using Lillogrenda.Charging.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApi();
builder.Services.AddApplication();

var app = builder.Build();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseSwaggerGen();
app.Run();