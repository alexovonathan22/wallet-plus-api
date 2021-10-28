using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlusApi.Core.Common.RouteDeclarations;
using WalletPlusApi.Core.Constants;
using WalletPlusApi.Core.Util.Email;
using WalletPlusApi.ExtensionClasses;
using WalletPlusApi.Infrastructure.Persistence;
using WalletPlusApi.Infrastructure.Services.Implementation;
using WalletPlusApi.Infrastructure.Services.Interfaces;

namespace WalletPlusApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connstr = Configuration.GetSection("ConnectionStrings:Wallet.ConnectionString").Value;
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddDbContext<WalletPlusApiContext>(options => options.UseSqlServer(connstr, c => c.MigrationsAssembly("WalletPlusApi.Infrastructure")));



            #region Auth/Auth Setup

            services.AddAppAuthentication("");
            services.AddAuthorization(opt =>
            {
                //Just the admin
                opt.AddPolicy(AuthorizedUserTypes.Admin, policy =>

                policy.RequireRole(Roles.Admin));

                // Just the user
                opt.AddPolicy(AuthorizedUserTypes.Customer, policy =>

                policy.RequireRole(Roles.Customer));
                // user and admin
                opt.AddPolicy(AuthorizedUserTypes.UserAndAdmin, policy =>

                policy.RequireRole(Roles.Customer, Roles.Admin));


            });

            #endregion
            services.AddHttpContextAccessor();
            services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiRoutes.Version, new OpenApiInfo { Title = "WalletPlusApi", Version = ApiRoutes.Version });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{ApiRoutes.Version}/swagger.json", $"WalletPlusApi {ApiRoutes.Version}"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
