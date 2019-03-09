using System;
using System.Data;
/* using Autofac;
using Autofac.Extensions.DependencyInjection; */
using Clinic.Authorize.Core;
using Clinic.Authorize.Core.Repositories;
//using Clinic.Authorize.Middlewares;
using Clinic.Authorize.Persistance;
using Clinic.Authorize.Persistance.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

public static class IServiceCollectionExtension
{
    public static void AddDefaultDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration["connectionString"]; 
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IDbTransaction>(p => {
            var connection = new NpgsqlConnection(connString);
            connection.Open();
            var transaction = connection.BeginTransaction();
            return transaction;
            }
        );
    }
}