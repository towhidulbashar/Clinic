using System;
using System.Data;
using Autofac;
using Autofac.Extensions.DependencyInjection;
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
    /* public static void AddDefaultDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration["connectionString"]; 
        services.AddTransient<IUserRepository, UserRepository>();
        //services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IDbTransaction>(p => {
            var connection = new NpgsqlConnection(connString);
            connection.Open();
            var transaction = connection.BeginTransaction();
            return transaction;
            }
        );

    } */
    public static IServiceProvider AddAutofacConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterType<UserRepository>()
            .As<IUserRepository>();
        containerBuilder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .WithParameter("connectionString", configuration["connectionString"]);

        containerBuilder.Populate(services);
        var container = containerBuilder.Build();
        return new AutofacServiceProvider(container);
    }
}