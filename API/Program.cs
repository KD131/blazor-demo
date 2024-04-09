using Core.Services;
using DotNetEnv;
using DotNetEnv.Configuration;
using Microsoft.EntityFrameworkCore;
using Model.Mapping;
using Repository;
using Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddDotNetEnv(options: LoadOptions.TraversePath());
// Reloads the env vars to redo the connection string prefix parsing.
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddCommandLine(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<DemoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Demo")));
// For generic repositories that take a DbContext, not a specific one.
builder.Services.AddScoped<DbContext, DemoContext>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(EntityDtoProfile));

// Error handling
builder.Services.AddProblemDetails();

// Services
builder.Services.AddScoped(typeof(IGenericMethodCrudService<,>), typeof(GenericMethodCrudService<,>));
builder.Services.AddScoped<IPostService, GenericMethodPostService>();

// Repositories
builder.Services.AddScoped(typeof(ICrudRepository<,>), typeof(EfCrudRepository<,>));
builder.Services.AddScoped<IPostRepository, PostRepository>();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}