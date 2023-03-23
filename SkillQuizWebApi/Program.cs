using Business_Logic_Layer;
using Business_Logic_Layer.Interface;
using Data_Access_Layer;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SkillQuizWebApi.ExceptionDealer;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillQuizzWebApi
{
    public class Program
    {
        //errorHandler General // MUST BE REDONE TO BE A ERROR 500 NOT 404
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                  IApiVersionDescriptionProvider provider)
        {
            // ...
            Middlewares(app, env);
        }            

        void Middlewares(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureExceptionHandler(env.IsDevelopment());
        }

        public static void Main(string[] args)
        {

            //CreateHostBuilder(args).Build().Run();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(policy => policy.AddPolicy("CorsPolicy", build =>
            {
                build.AllowAnyOrigin() //ALLOWING ALL ORIGINS BUT WITH RESTRICT IT TO A SET OR DOMAINS
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            }));
            builder.Services.AddControllers();
            builder.Services.Configure<JsonOptions>(options =>
             {
                 options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
             });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddScoped<InterfaceQuestion, QuestionBLL>();
            builder.Services.AddScoped<InterfaceLanguages, LanguagesBLL>();

            var app = builder.Build();

            //// Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // app.UseHttpsRedirection(); IF ACTIVE IT THROWS CORS ERROR ONLY ACTIVATE THIS IN PROD
            app.UseStaticFiles();
            app.UseRouting();

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors("CorsPolicy");

            //app.UseAuthentication(); // This need to be added	
            app.UseAuthorization();
            app.MapControllers();
            app.Run();


        }


    }
}
