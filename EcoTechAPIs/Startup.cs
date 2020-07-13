using Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;


namespace EcoTechAPIs
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            if (Configuration.GetSection("GetCorsIPs").Exists())
            {
                // Retrieve list of IPs from appsetting.json file to allow the CORS.
                string[] origins = Configuration.GetSection("GetCorsIPs").GetValue(typeof(string), "IPs").ToString().Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
                
                // Use Cors Methods to resolve CORS 
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsApi",
                        builder => builder.WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod());
                });
            }

            // Use PropertyNamingPolicy to render JSON in the same Case as the Properties created in BAL.
            services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.Configure<CookiePolicyOptions>(options => { options.CheckConsentNeeded = context => true; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsApi");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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