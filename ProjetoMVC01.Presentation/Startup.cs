using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoMVC01.Repository.Interfaces;
using ProjetoMVC01.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC01.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //componente para ler o conteudo do arquivo /appsettings.json
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //definir que o modo de navegação do projeto web é MVC (Controllers e Views)
            services.AddControllersWithViews();

            //Habilitando o uso de cookies e tambem autenticação
            services.Configure<CookiePolicyOptions>(options => { options.MinimumSameSitePolicy = SameSiteMode.None; });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            //ler a connectionstring contida no arquivo /appsettings.json
            var connectionstring = Configuration.GetConnectionString("ProjetoMVC01");

            //inserir a connectionstring dentro da classe TarefaRepository (injeção de dependencia) 
            services.AddTransient<ITarefaRepository>
                (config => new TarefaRepository(connectionstring));

            //inserir a connectionstring dentro da classe UsuarioRepository (injeção de dependencia)
            services.AddTransient<IUsuarioRepository>
                (config => new UsuarioRepository(connectionstring));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //habilitar a pasta /wwwroot
            app.UseStaticFiles();

            app.UseRouting();

            //autenticação no projeto..
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            //mapeando a página inicial do projeto
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}"
                    );
            });
        }
    }
}
