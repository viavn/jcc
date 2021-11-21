using JccApi.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;

namespace JccApi.Extensions
{
    public static class DataBaseConfigurationExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = Environment.GetEnvironmentVariable("ConnectionString_Db");
            var connectionString = configuration.GetConnectionString("Db");
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            services.AddDbContext<DataBaseContext>(options =>
                options.UseNpgsql(builder.ConnectionString)
                    //.UseLoggerFactory(
                    //    LoggerFactory.Create(
                    //        b => b
                    //        .AddConsole()
                    //        .AddFilter(level => level >= LogLevel.Information)))
                    //.EnableSensitiveDataLogging()
                    //.EnableDetailedErrors()
                    );

            return services;
        }
    }
}
