using AutoMapper;
using JobListing.Common;
using JobListing.Common.Helper;
using JobListing.Common.Settings;
using JobListing.Core.Implementation;
using JobListing.Core.Interface;
using JobListing.Core.Services;
using JobListing.Data.EFCore;
using JobListing.Data.EFCore.Implementation;
using JobListing.Data.EFCore.Interface;
//using JobListing.Data.Repositories.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing
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
            services.AddControllers().AddNewtonsoftJson(
                    options => {
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    }
                );   
            services.AddDbContextPool<JobListingContext>(option =>
            option.UseSqlite(Configuration.GetConnectionString("EfCore")));

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<JobListingContext>()
            .AddDefaultTokenProviders();


            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddMvc().AddNewtonsoftJson();
            services.AddScoped<ICvRepository, CvRepository>();
            services.AddScoped<ICvService, CvService>();
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IADOOperations, ADOOperation>();
            services.AddTransient<SeederClass>();
           // services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<JobListingContext>();



            services.AddAutoMapper();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobListing", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Job Listing Project API"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { 
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
                });
        });
            services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }

                ).AddJwtBearer(
                option =>
                {
                    var param = new TokenValidationParameters();
                    param.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTKey"]));
                    param.ValidateIssuer = false;
                    param.ValidateAudience = false;
                    option.TokenValidationParameters = param;

                }
                );
                                          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeederClass seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            //seed.SeedMe().Wait();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobListing v1"));

        }
    }
}
