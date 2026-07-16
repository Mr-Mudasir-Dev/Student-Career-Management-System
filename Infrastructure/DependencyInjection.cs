using Application.Interface;
using Application.Interface.Repository;
using CMS.Infrastructure.Data;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Mapping;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
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
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {

            // Database 
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            // Identiy
            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequiredLength = 6;
                option.Password.RequireUppercase = true;
                option.Password.GetHashCode();

            })
             .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Auto Mapper 
            services.AddAutoMapper(cfg => { }, typeof(IdentityProfile).Assembly);

            // UOW Register
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repository Register
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();

            return services;
        }
    }
}
