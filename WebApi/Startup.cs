using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.BusinessLogicImpl;
using BusinessLogic.IBusinessLogic;
using DataAccess.Context;
using DataAccess.IRepositories;
using DataAccess.RepositoriesImpl;
using DTO.Models.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Common.Constant;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using ContractInteraction.ContractServices;
using Common.Utils;
using AutoMapper;
using Common.Mapper;
using System.Reflection;
using System.IO;

namespace CommonWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt =>
                {
                    var resolver = opt.SerializerSettings.ContractResolver;
                    if (resolver != null)
                    {
                        var res = resolver as DefaultContractResolver;
                        res.NamingStrategy = null;
                    }
                });

            //Auto mapper
            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfiles>());
            services.AddAutoMapper();

            #region Instanceinjection

            // Repositories
            services.AddScoped<IUserRepository, UserRepositoryImpl>();
            services.AddScoped<IRoleRepository, RoleRepositoryImpl>();
            services.AddScoped<IFoodRepository, FoodRepositoryImpl>();
            services.AddScoped<ICategoryRepository, CategoryRepositoryImpl>();
            services.AddScoped<IFoodRepository, FoodRepositoryImpl>();
            services.AddScoped<ITreatmentRepository, TreatmentRepositoryImpl>();
            services.AddScoped<IPremisesRepository, PremisesRepositoryImpl>();
            services.AddScoped<ITransactionRepository, TransactionRepositoryImpl>();
            services.AddScoped<IFoodDetailTypeRepository, FoodDetailTypeRepositoryImpl>();
            services.AddScoped<IDistributorFoodRepository, DistributorFoodRepositoryImpl>();
            services.AddScoped<IProviderFoodRepository, ProviderFoodRepositoryImpl>();
            services.AddScoped<ITransactionStatusRepository, TransactionStatusRepositoryImpl>();
            services.AddScoped<IFeedingRepository, FeedingRepositoryImpl>();
            services.AddScoped<IVaccineRepository, VaccineRepositoryImpl>();
            services.AddScoped<IFoodDetailRepository, FoodDetailRepositoryImpl>();
            services.AddScoped<IFeedingFoodRepository, FeedingFoodRepositoryImpl>();
            services.AddScoped<IPremisesTypeRepository, PremisesTypeRepositoryImpl>();
            services.AddScoped<IRegisterInfoRepository, RegisterInfoRepositoryImpl>();
            services.AddScoped<IVaccineFoodRepository, VaccineFoodRepositoryImpl>();

            //BusinessLogic
            services.AddScoped<IUserBL, UserBLImpl>();
            services.AddScoped<IRoleBL, RoleBLImpl>();
            services.AddScoped<IFoodBL, FoodBLImpl>();
            services.AddScoped<IFoodDataBL, FoodDataBLImpl>();
            services.AddScoped<ITransactionBL, TransactionBLImpl>();
            services.AddScoped<IPremisesBL, PremisesBLImpl>();
            services.AddScoped<ITreatmentBL, TreatmentBLImpl>();
            services.AddScoped<IFoodDetailBL, FoodDetailImpl>();
            services.AddScoped<ITreatmentBL, TreatmentBLImpl>();
            services.AddScoped<IFeedingBL, FeedingBLImpl>();
            services.AddScoped<IVaccineBL, VaccineBLImpl>();
            services.AddScoped<IPremisesTypeBL, PremisesTypeBLImpl>();
            services.AddScoped<IRegisterInfoBL, RegisterInfoBLImpl>();
            services.AddSingleton<IEmailSender, EmailSender>();
            //Service
            services.AddScoped<IContractServices, ContractServicesImpl>();

            #endregion

            services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("AuthMessageSenderOptions"));
            services.AddSingleton<IEmailSender, EmailSender>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Food Tracker Common API", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new ApiKeyScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<SecurityRequirementsOperationFilter>(); 
            });

            //Set database.
            services.AddDbContext<FoodTrackingDbContext>(c =>
                    c.UseSqlServer(Configuration.GetConnectionString(AppConstant.DB_CONNECT)));

            //Jwt Authentication

            var key = Encoding.UTF8.GetBytes(Configuration["JWTSetttings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = Configuration["JWTSetttings:Client_URL"],
                    ValidAudience = Configuration["JWTSetttings:Client_URL"],
                };
            });

            services.AddCors(option =>
            {
            option.AddPolicy(MyAllowSpecificOrigins,
                buidder => {
                    buidder.WithOrigins("http://localhost:5000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            //Inject AppSettings
            services.Configure<JWTSetttings>(Configuration.GetSection("JWTSetttings"));

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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseMvc();
        }
    }
}
