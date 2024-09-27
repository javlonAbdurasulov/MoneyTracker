using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyTracker.Infrastructure.Data;
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
        }
    }
}
