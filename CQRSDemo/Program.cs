using CQRSCartAPI.Contexts;
using CQRSDemo.Commands;
using CQRSDemo.Events;
using CQRSDemo.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CatalogContext>
               (options => options.UseSqlite
               (configuration.GetConnectionString("SqliteConnection")));
builder.Services.AddTransient<CatalogSqliteRepository>();
builder.Services.AddTransient<CatalogMongoRepository>();
builder.Services.AddTransient<AMQPEventPublisher>();
builder.Services.AddSingleton<CatalogMessageListener>();
builder.Services.AddScoped<ICommandHandler<Command>, CatalogCommandHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
