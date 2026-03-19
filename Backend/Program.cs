using System.Text;
using System.Text.Json.Serialization;
using InventoryAssetTracking;
using InventoryAssetTracking.Models;
using InventoryAssetTracking.Repositories;
using InventoryAssetTracking.Repositories.Interfaces;
using InventoryAssetTracking.Services;
using InventoryAssetTracking.Services.Interfaces;
using InventoryAssetTracking.Tools;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Set up dependency injection
builder.Services.AddTransient<IdentitySeeder>();
builder.Services.AddTransient<AssetQrGenerator>();

builder.Services.AddScoped<EntityChecker>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ICheckoutService, CheckoutService>();
builder.Services.AddScoped<ICheckoutRepository, CheckoutRepository>();

builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();

builder.Services.AddScoped<IAssetHistoryService, AssetHistoryService>();
builder.Services.AddScoped<IAssetHistoryRepository, AssetHistoryRepository>();

// Set up automapping between DTOs and models
builder.Services.AddMapster();


// Set up connection to db
var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnvLoader.Load(dotenv);
var connectionString = DotEnvLoader.GenerateConnectionString();

builder.Services.AddDbContext<InventoryAssetContext>(options =>
    options.UseNpgsql(connectionString));

// Set up JWT authentication
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
var key = Encoding.UTF8.GetBytes(jwtKey!); // jwtKey cannot be null, as DotEnvLoader.Load already checks this

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.ContainsKey("AuthToken"))
                    context.Token = context.Request.Cookies["AuthToken"];
                
                return Task.CompletedTask;
            }
        };
    });


// Set up Identity Core
builder.Services.AddIdentityCore<User>(options =>
    {
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<InventoryAssetContext>()
    .AddDefaultTokenProviders();

// Set up CORS
var ApplicationFrontend = "_applicationFrontend";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: ApplicationFrontend,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetService<IdentitySeeder>();
    if (seeder == null)
        throw new InvalidOperationException("Could not access IdentitySeeder to initialise Admin profiles");

    await seeder.SeedRolesAsync();
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(ApplicationFrontend);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();