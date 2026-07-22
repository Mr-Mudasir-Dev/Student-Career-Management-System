using Api.Middleware;
using Application;
using Application.Common;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Token daalo: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
    };

    // Custom Error Response
    options.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            // Default error band
            context.HandleResponse();

            // Apna custom response
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";

            var response = ApiResponse<object>.UnauthorizedResponse();

            await context.Response.WriteAsJsonAsync(response);
        },

        
        OnForbidden = async context =>
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";

            var response = ApiResponse<object>.ForbiddenResponse();
            await context.Response.WriteAsJsonAsync(response);
        }
    };
});

builder.Services.AddAuthorization();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JWT"));

var app = builder.Build();
// Seed the database with roles and admin user
await app.Services.SeedRoleDatabaseAsync();

await app.Services.SeedAdminDatabaseAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();