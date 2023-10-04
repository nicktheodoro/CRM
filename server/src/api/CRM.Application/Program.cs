using Microsoft.EntityFrameworkCore;
using MyApp.Core.Users.Data.MySql.Contexts;
using MyApp.Core.Users.Data.MySql.Repositories;
using MyApp.Core.Users.Handlers;
using MyApp.Core.Users.Interfaces;
using MyApp.Core.Users.Mappers;
using MyApp.Core.Users.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder => builder
        .AllowAnyOrigin()
        //.WithOrigins("http://localhost:8080") // Defina aqui o domínio do seu cliente
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));



// Dependence injections
builder.Services.AddAutoMapper(typeof(UserMap));
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(UserHandler).Assembly));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DomainService>();
builder.Services.AddScoped<UserHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowOrigin");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
