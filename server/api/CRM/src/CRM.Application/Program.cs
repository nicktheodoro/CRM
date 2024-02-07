using CRM.Application.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyApp.Core.Users.Data.MySql.Contexts;
using MyApp.Core.Users.Data.MySql.Repositories;
using MyApp.Core.Users.Handlers;
using MyApp.Core.Users.Interfaces;
using MyApp.Core.Users.Mappers;
using MyApp.Core.Users.Services;
using Product.Core.Handlers;
using Product.Core.Interfaces;
using Product.Core.Mappers;
using Product.Core.Services;
using Product.Data.MySql.Contexts;
using Product.Data.MySql.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JTW Authorization header using the Beaerer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            Array.Empty<string>()
        }
    });
});

builder.Services.Configure<TokenConfiguration>(builder.Configuration.GetSection(TokenConfiguration.Section));
builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder.AllowAnyOrigin()
                                               .AllowAnyHeader()
                                               .AllowAnyMethod());
});

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(UserHandler).Assembly));
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(ProductHandler).Assembly));
builder.Services.AddAutoMapper(typeof(UserMap));
builder.Services.AddAutoMapper(typeof(ProductMap));

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserContext>(options => options.UseNpgsql(connection, b => b.MigrationsAssembly("User.Core")));
builder.Services.AddDbContext<ProductContext>(options => options.UseNpgsql(connection, b => b.MigrationsAssembly("Product.Core")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserHandler>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
