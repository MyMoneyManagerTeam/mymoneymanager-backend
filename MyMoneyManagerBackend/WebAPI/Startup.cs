using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Application.Services.Accounts;
using Application.Services.Jars;
using Application.Services.Transactions;
using Application.Services.Users;
using Infrastructure.SqlServer.Accounts;
using Infrastructure.SqlServer.Auth;
using Infrastructure.SqlServer.Jars;
using Infrastructure.SqlServer.Transactions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyMoneyManagerBackend.Controllers;
using MyMoneyManagerBackend.Utils;

namespace MyMoneyManagerBackend
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c => {  
                c.SwaggerDoc("mymoneymanager", new OpenApiInfo {  
                    Version = "v1",  
                    Title = "My Money Manager",  
                    Description = "API de notre projet My Money manager",  
                    Contact = new OpenApiContact() {  
                        Name = "MyMoneyManager Team", 
                        Email = "lopez.lucas@hotmail.com", 
                        Url = new Uri("https://github.com/MyMoneyManagerTeam")  
                    }  
                });
                // Bearer token authentication
                var securityDefinition = new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };
                c.AddSecurityDefinition("jwt_auth", securityDefinition);

                // Make sure swagger UI requires a Bearer token specified
                var securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "jwt_auth",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                var securityRequirements = new OpenApiSecurityRequirement()
                {
                    {securityScheme, new string[] { }},
                };
                c.AddSecurityRequirement(securityRequirements);
            });  
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                })
                .AddJwtBearer("JwtBearer", jwtOptions =>
                {
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = AuthUtils.SIGNING_KEY,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });
            
            services.AddCors(options => //ajout du CORS aux services
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            services.AddControllers();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<IJarService, JarService>();
            services.AddSingleton<IJarRepository, JarRepository>();
            services.AddSingleton<ITransactionService, TransactionService>();
            services.AddSingleton<ITransactionRepository, TransactionRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/mymoneymanager/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins); //ici in a ajouté le CORS

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}