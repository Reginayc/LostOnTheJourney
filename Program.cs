using Microsoft.EntityFrameworkCore;
using LOSTONTHEJOURNEY.Models.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddDbContext<LostOnTheJourneyContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LOST ON THE JOURNEY API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LOST ON THE JOURNEY API v1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
