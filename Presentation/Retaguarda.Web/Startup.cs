using AutoMapper;
using FiveSys.Retaguarda.Infrastructure.CrossCutting.IoC;
using FiveSys.Retaguarda.Infrastructure.Data.Acesso;
using FiveSys.Retaguarda.Infrastructure.Data.Admin;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace FiveSys.Presentation.Retaguarda.Web
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RetaguardaAdminContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("RetaguardaContext")));

            services.AddDbContext<RetaguardaAcessoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("RetaguardaContext")));

            services.AddMvc();

            services.AddDistributedMemoryCache();

            services.AddSession();

            services.AddAutoMapper(typeof(FileSys.Retaguarda.Application.Admin.ConfigurationProfile).GetTypeInfo().Assembly,
                typeof(FileSys.Retaguarda.Application.Acesso.ConfigurationProfile).GetTypeInfo().Assembly);

            Configuracao.InjecaoDependencia(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("pt-BR"),
                },
                SupportedUICultures = new List<CultureInfo>
                {
                    new CultureInfo("pt-BR"),
                }
            });

            app.UseStaticFiles();

            app.UseSession();

            //app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
