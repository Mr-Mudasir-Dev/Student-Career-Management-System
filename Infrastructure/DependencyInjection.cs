using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Service;
using CMS.Infrastructure.Data;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Mapping;

using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Infrastructure
{
    public static class DependencyInjection
    {

        // Seeding ke liye alag method
        public static async Task SeedRoleDatabaseAsync(this IServiceProvider serviceProvider)
        {
            var Scoped = serviceProvider.CreateScope();
            var roleManager = Scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await RoleSeeder.SeedRolesAsync(roleManager);

        }
        // Add Admin Seeder
        public static async Task SeedAdminDatabaseAsync(this IServiceProvider serviceProvider)
        {
            var Scoped = serviceProvider.CreateScope();
            var userManager = Scoped.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = Scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await AdminSeeder.SeederAdminAsync(userManager,roleManager);
        }
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {

            // Database 
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });


            // AddIdentityCore 

            services.AddIdentityCore<ApplicationUser>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequiredLength = 6;
                option.Password.RequireUppercase = true;
            })
            .AddRoles<IdentityRole>()              // ← Roles ke liye
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager();



            // Auto Mapper 
            services.AddAutoMapper(cfg => { }, typeof(IdentityProfile).Assembly);

            // services
            services.AddScoped<IJwtService, JwtService>();

            // UOW Register
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repository Register
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();

            return services;
        }
    }
}
