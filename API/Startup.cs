using API.Config;
using Application.Employees;
using DataStore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace API
{
    public class Startup
    {
        private readonly ReactivitiesConfiguration _reactiveConfiguration;
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _reactiveConfiguration = new ReactivitiesConfiguration();
            configuration.Bind("ReactivitiesConfiguration", _reactiveConfiguration);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddControllers().AddNewtonsoftJson(
                opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSingleton(_reactiveConfiguration);
            services.AddSingleton<DocumentStoreHolder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
