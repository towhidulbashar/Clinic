using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Clinic.Api.Core;
using Clinic.Api.Core.Repositories;
using Clinic.Api.Middlewares;
using Clinic.Api.Persistance;
using Clinic.Api.Persistance.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionExtension
{
    public static IServiceProvider AddAutofacConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterType<PatientRepository>()
            .As<IPatientRepository>();
        containerBuilder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .WithParameter("connectionString", configuration["connectionString"]);
        containerBuilder.RegisterType<ErrorHandlingMiddleware>()
            .As<IErrorHandlingMiddleware>();
        containerBuilder.Populate(services);
        var container = containerBuilder.Build();
        return new AutofacServiceProvider(container);
    }
}