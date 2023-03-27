using Business_Logic_Layer;
using Business_Logic_Layer.Interface;
using Data_Access_Layer;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SkillQuizWebApi.ExceptionDealer;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            builder.Services.AddControllers()
                .AddNewtonsoftJson();
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
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero //this test to avoid waiting a long time for the token to be considered actually expired.
                };                
            });
            builder.Services.AddAuthorization();
            builder.Services.AddScoped<InterfaceDomain, DomainBLL>();
            builder.Services.AddScoped<InterfaceUserType, UserTypeBLL>();
            builder.Services.AddScoped<InterfaceElementTranslation, ElementTranslationBLL>();
            builder.Services.AddScoped<InterfaceDomainCompose, DomainComposeBLL>();
            builder.Services.AddScoped<InterfaceSubDomain, SubDomainBLL>();
            builder.Services.AddScoped<InterfaceTestStatus, TestStatusBLL>();
            builder.Services.AddScoped<InterfaceAnswer, AnswerBLL>();
            builder.Services.AddScoped<InterfaceQuestion, QuestionBLL>();
            builder.Services.AddScoped<InterfaceQuiz, QuizBLL>();
            builder.Services.AddScoped< InterfaceTestCategory, TestCategoryBLL>();
            builder.Services.AddScoped<InterfaceTest, TestBLL>();
            builder.Services.AddScoped<InterfaceTestCompose, TestComposeBLL>();
            builder.Services.AddScoped<InterfaceAnswerQuestion, AnswerQuestionBLL>();
            builder.Services.AddScoped<InterfaceLanguage, LanguageBLL>();
            builder.Services.AddScoped<InterfaceUser, UserBLL>();
            builder.Services.AddScoped<InterfaceQuizCompose, QuizComposeBLL>();
            builder.Services.AddScoped<InterfaceTestUser, TestUserBLL>();
            builder.Services.AddScoped<InterfaceQuizUser, QuizUserBLL>();
            builder.Services.AddScoped<InterfaceQuestionUser, QuestionUserBLL>();
            builder.Services.AddScoped<InterfaceAnswerUser, AnswerUserBLL>();

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
            app.UseAuthentication(); // This need to be added	
            app.UseAuthorization();
            app.MapControllers();
            app.Run();


        }


    }
}
