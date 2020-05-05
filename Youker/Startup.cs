using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Youker.Application;
using Youker.Entity.Context;
using Youker.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Youker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<YoukerContext, YoukerContext>(sp =>
            {
                return new YoukerContext();
            });

            #region 跨域
            services.AddCors(options =>
            {
                //services.AddCors(options =>
                //{
                //    options.AddPolicy("CorsPolicy-public",
                //        builder => builder.AllowAnyOrigin()   //WithOrigins and define a specific origin to be allowed (e.g. https://mydomain.com)
                //            .AllowAnyMethod()
                //            .AllowAnyHeader()
                //            .AllowCredentials()
                //    .Build());
                //});
                options.AddPolicy("CorsPolicy-public",
                    builder =>
                        builder.SetIsOriginAllowed(_ => true)   //WithOrigins and define a specific origin to be allowed (e.g. https://mydomain.com)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                .Build());
                //options.AddPolicy(name: "CorsPolicy-public",
                //             builder =>
                //             {
                //                 builder.WithOrigins("http://localhost:8082",
                //                                     "http://122.51.15.158:8002")
                //                 .AllowAnyHeader()
                //                 .AllowAnyMethod();
                //             });
            });
            #endregion

            services.AddControllers();

            #region JWT
            services.Configure<TokenConfig>(Configuration.GetSection("tokenConfig"));
            var token = Configuration.GetSection("tokenConfig").Get<TokenConfig>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            #endregion

            #region 注入
            services.AddTransient<DBContextBase, DBContextBase>();
            //services.AddScoped(typeof(DBContextBase));
            Assembly rpAssembly = Assembly.Load("Youker.Repository");
            foreach (var implement in rpAssembly.GetTypes())
            {
                services.AddTransient(implement);
            }
            Assembly assembly = Assembly.Load("Youker.Service");
            foreach (var implement in assembly.GetTypes())
            {
                services.AddTransient(implement);
            }
            #endregion

            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Youker", Version = "v1" });
                //名称必须为“Bearer”，否者请求接口时Header不会带上Token
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 在下方输入Bearer {token} 即可，注意两者之间有空格。",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "Youker.Api.xml");
                c.IncludeXmlComments(xmlPath);
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();

            app.UseCors("CorsPolicy-public");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Youker V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
