using BackendUndergradFinal.AutoMapperProfiles;
using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Services;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BackendUndergradFinal
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
            services.AddDbContext<MyEfDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<WaterUser, IdentityRole<Guid>>(opt =>
                {
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequiredUniqueChars = 0;
                    opt.Password.RequiredLength = 8;
                    opt.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<MyEfDbContext>()
                .AddDefaultTokenProviders();


            services.AddAuthentication(options => 
                    {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddAuthorization();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackendUndergradFinal", Version = "v1" });
            });
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile<AccountProfile>();
                opt.AddProfile<SourceVariantProfile>();
                opt.AddProfile<SourcePlaceProfile>();
                opt.AddProfile<SourceContributionProfile>();
            });

            // Services
            services.AddScoped<WaterUserService>();
            services.AddScoped<MediaService>();
            services.AddScoped<WaterSourceVariantService>();
            services.AddScoped<WaterSourcePlaceService>();
            services.AddScoped<WaterSourceContributionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Local"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(c => c.RouteTemplate = "api/swagger/{documentName}/swagger.json");
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "BackendUndergradFinal v1");
                    c.RoutePrefix = $"api/swagger";

                });
            }

            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Items["Exception"] = contextFeature.Error.Message;
                        context.Items["StackTrace"] = contextFeature.Error.StackTrace;
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            BadRequestException _ => (int)HttpStatusCode.BadRequest,
                            _ => (int)HttpStatusCode.InternalServerError
                        };

                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(contextFeature.Error.Message));
                    }
                });
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            var roleManager =
                (RoleManager<IdentityRole<Guid>>)app.ApplicationServices.GetService(
                    typeof(RoleManager<IdentityRole<Guid>>));
            if (!roleManager.Roles.Any())
            {
                var role = new IdentityRole<Guid>
                {
                    Name = "Admin"
                };
                var res = roleManager.CreateAsync(role).Result;
                role = new IdentityRole<Guid>
                {
                    Name = "User"
                };
                res = roleManager.CreateAsync(role).Result;
            }

            var userManager = (UserManager<WaterUser>)app.ApplicationServices.GetService(typeof(UserManager<WaterUser>));

            if (userManager.FindByEmailAsync("admin@waterfountains.com").Result == null)
            {
                var admin = new WaterUser
                {
                    Email = "admin@waterfountains.com",
                    UserName = "Admin"
                };
                var create = userManager.CreateAsync(admin, "Abcd_1234").Result;

                if (!create.Succeeded)
                    throw new Exception(create.Errors.Select(x => x.Description).Aggregate((x, y) => x + "\n" + y));

                var res = userManager.AddToRoleAsync(admin, "Admin").Result;

                if (!res.Succeeded)
                    throw new Exception(res.Errors.Select(x => x.Description).Aggregate((x, y) => x + "\n" + y));
            }
        }
    }
}
