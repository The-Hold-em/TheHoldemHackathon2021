
using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using THH.Services.MainApi.BLL.Seeding;
using THH.Services.MainApi.BLL.Settings;
using THH.Services.MainApi.DAL.Concrete.EntityFrameworkCore.Contexts;
using THH.Shared.BLL.Interfaces;
using THH.Shared.BLL.Managers;
using THH.Shared.Core.ExtensionMethods;
using THH.Shared.Core.Services;
using THH.Shared.Core.Services.Interfaces;
using THH.Shared.DAL.Concrete.EntityFrameworkCore.Repositories;
using THH.Shared.DAL.Interfaces;

namespace THH.Services.MainApi.BLL.Containers.MicrosoftIOC;

public static class MicrosoftIocExtension
{

    public static void AddDependencies(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        string connectionString = configuration.GetCustomConnectionString(environment.GetConnectionType());
        string migrationName = "THH.Services.MainApi";

        services.AddTransient<DbContext, ApplicationDbContext>();

        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseSqlServer(connectionString, sqlOpt =>
                sqlOpt.MigrationsAssembly(migrationName)
                )
        );

        services.AddHttpContextAccessor();

        #region Repositoryies
        services.AddTransient(typeof(IGenericCommandRepository<>), typeof(EfGenericCommandRepository<>));
        services.AddTransient(typeof(IGenericQueryRepository<>), typeof(EfGenericQueryRepository<>));
        #endregion
        #region Services
        services.AddTransient(typeof(IGenericQueryService<>), typeof(GenericQueryManager<>));
        services.AddTransient(typeof(IGenericCommandService<>), typeof(GenericCommandManager<>));

        //services.AddScoped<IService, Manager>();
        services.AddScoped<ISharedIdentityService, SharedIdentityService>();


        services.AddScoped<DefaultRecords>();
        services.AddScoped<DefaultsSeeder>();
        #endregion


        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<ICustomMapper, CustomMapper>();

        services.AddSetting<CitySetting>(configuration, "CitySetting");

    }

    public static void AddValidationDependencies(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.AddFluentValidation(opt => { }
        //opt.RegisterValidatorsFromAssemblyContaining<ValidationLayer>()
        );
    }
}
