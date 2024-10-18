using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyTracker.Application.Interfaces.Repository;
using MoneyTracker.Application.Interfaces.Service;
using MoneyTracker.Application.Services;
using MoneyTracker.Domain.Models.DTO;
using MoneyTracker.Domain.Models.Entity;
using MoneyTracker.Infrastructure.Data;
using MoneyTracker.Infrastructure.Filter;
using MoneyTracker.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Infrastructure.AppConfiguration
{
    public static class ConfigurationServices
    {
        public static void AddConfigurationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option => option.UseNpgsql(configuration.GetConnectionString("Default")));

            //repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository,TransactionRepository>();
            services.AddScoped<ICategoryRepository,CategoryRepository>();



            //services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();

            //filter
            services.AddScoped<IFilterService, FilterService>();
            
        }
    }
}
