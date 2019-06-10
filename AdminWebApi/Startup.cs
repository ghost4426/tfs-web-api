using DataAccess.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using DataAccess.IRepositories;
using DataAccess.RepositoriesImpl;
using BusinessLogic.IBusinessLogic;
using BusinessLogic.BusinessLogicImpl;
using Newtonsoft.Json.Serialization;

namespace AdminWebApi
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

            //Also make top level configuration available (for EF configuration and access to connection string)
            services.AddSingleton(Configuration); //IConfigurationRoot
            services.AddSingleton<IConfiguration>(Configuration);


            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt =>
                {
                    var resolver = opt.SerializerSettings.ContractResolver;
                    if (resolver != null)
                    {
                        var res = resolver as DefaultContractResolver;
                        res.NamingStrategy = null;
                    }
                });

            //Instance injection
            // Repositories
            services.AddScoped<IUserRepository, UserRepositoryImpl>();

            //BusinessLogic
            services.AddScoped<IUserBL, UserBLImpl>();

            //Set Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Food Tracker API", Version = "v1" });
            });


            //Set database.
            services.AddDbContext<FoodTrackerDbContext>(c =>
                    c.UseSqlServer(Configuration.GetConnectionString("FoodTrackerDbConnection_Local")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Food Tracker API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
