using Microsoft.Extensions.DependencyInjection;
using MongoDBTest.Models;
using MongoDBTest.MongoDB;
using MongoDBTest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<DooProjectMongoDbSetting>(
    builder.Configuration.GetSection("DooProjectMongoDb"));

builder.Services.AddSingleton(typeof(MongoDbConnection<>));
builder.Services.AddSingleton(typeof(CRUDServices<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
