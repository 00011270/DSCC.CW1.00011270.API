using BlogPlatform.DAL;
using BlogPlatform.Models;
using BlogPlatform.Repository;
using Microsoft.EntityFrameworkCore;

// Project made by 00011270
// For CC module level 6 WIUT
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding DbContext to register database context and establishing connection with the Database
builder.Services.AddDbContext<BlogContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("BloggingDb"), o => o.EnableRetryOnFailure(10));
});

// Registering the Repository Classes as an implementation of interface with DI (dependency inject)
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IRepository<Post>, PostRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
