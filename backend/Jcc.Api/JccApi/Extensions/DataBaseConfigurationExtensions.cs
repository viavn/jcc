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
            string connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
               ? configuration.GetConnectionString("Db")
               : GetProdConnectionString();

            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            services.AddDbContext<DataBaseContext>(options =>
                options.UseNpgsql(builder.ConnectionString)
                    .UseLoggerFactory(
                        LoggerFactory.Create(
                            b => b
                            .AddConsole()
                            .AddFilter(level => level >= LogLevel.Information)))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                    );

            return services;
        }

        private static string GetProdConnectionString()
        {
            return Environment.GetEnvironmentVariable("DbConnectionString");
        }
        

        // private static string GetHerokuConnectionString()
        // {
        //     // Get the connection string from the ENV variables
        //     string connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

        //     if (connectionUrl is null)
        //     {
        //         throw new ArgumentNullException("DATABASE_URL", "DATABASE_URL é nulo");
        //     }

        //     // parse the connection string
        //     var databaseUri = new Uri(connectionUrl);

        //     string db = databaseUri.LocalPath.TrimStart('/');
        //     string[] userInfo = databaseUri.UserInfo.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

        //     return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        // }
    }
}
