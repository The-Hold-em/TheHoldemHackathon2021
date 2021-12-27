
using Duende.IdentityServer.Services;

using FluentValidation.AspNetCore;

using THH.IdentityServer.Data;
using THH.IdentityServer.Mapping.AutoMapper;
using THH.IdentityServer.Models;
using THH.IdentityServer.Services;
using THH.Shared.Core.ExtensionMethods;
using THH.Shared.Core.Filters;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace THH.IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public string GetConnectionString() => Configuration.GetCustomConnectionString(Environment.GetConnectionType());

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = GetConnectionString();
            string migrationName = this.GetType().Namespace;

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddSingleton<ICorsPolicyService>((container) => {
                var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
                return new DefaultCorsPolicyService(logger)
                {
                    AllowAll = true
                };
            });

            services.AddLocalApiAuthentication();

            services.AddControllers(opt =>
            {
                opt.Filters.Add<ValidateModelAttribute>();
            }).AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<Startup>();
            });


            services.AddAutoMapper(typeof(ApplicationUserMapProfile));


            services.AddCustomValidationResponse();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));


            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<CustomIdentityErrorDescriber>();

            IIdentityServerBuilder builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                options.EmitStaticAudienceClaim = true;
            })
                .AddConfigurationStore(opt =>
                    opt.ConfigureDbContext = cOpt =>
                        cOpt.UseSqlServer(connectionString, sqlOpt =>
                            sqlOpt.MigrationsAssembly(migrationName)
                        )
                )
                .AddOperationalStore(opt =>
                    opt.ConfigureDbContext = cOpt =>
                        cOpt.UseSqlServer(connectionString, sqlOpt =>
                            sqlOpt.MigrationsAssembly(migrationName)
                        )
                )
                .AddAspNetIdentity<ApplicationUser>();

            builder.AddDeveloperSigningCredential()
                .AddProfileService<CustomProfileService>()
                .AddResourceOwnerValidator<IdentityResourceOwnerPasswordValidator>()
                .AddExtensionGrantValidator<TokenExchangeExtensionGrantValidator>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseDelayRequestDevelopment();
            }
            app.UseCustomExceptionHandler();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}