using Business_Logic_Layer;
using Business_Logic_Layer.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillQuizzWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

           var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            //{
            //    builder.WithOrigins("http://localhost:4200/").AllowAnyMethod().AllowAnyHeader();
            //}));

            //test


            //builder.Services.AddDbContext<FullStackDbContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("FullStackConnectionString")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("ApiCorsPolicy");

            app.UseCors(build =>
            {
                build.WithOrigins("http://localhost:4200/");
                build.AllowAnyMethod();
                build.AllowAnyOrigin();
                build.AllowAnyHeader();


            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                }).ConfigureServices(services =>
                {
                    services.AddScoped<InterfacePerson, PersonBLL>();
                });
    }
}
