using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SimpleWebApiTests.Interfaces;
using SimpleWebApiTests.Repositories;
using SimpleWebApiTests.Services;
using System.IO;

namespace SimpleWebApiTests
{
    public class Startup
    {
        private const string NAME_CONNECTION = "ApiConnection";
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IWebHostEnvironment environment)
        {
            _environment = environment;

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(_environment.IsDevelopment() ? $"appsettings.{_environment.EnvironmentName}.json" : "appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IContextDapper>(x => new ContextDapper(_configuration.GetConnectionString(NAME_CONNECTION)));
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IClienteServico, ClienteServico>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SimpleWebApiTests", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleWebApiTests v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
