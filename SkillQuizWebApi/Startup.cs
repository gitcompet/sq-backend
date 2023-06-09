using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Data_Access_Layer.Repository;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Hosting;


namespace SkillQuizzWebApi
{
    public class Startup
    {
        static string policyName = "ApiCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseMySQL(connectionString);

            //});

            //services.AddDefaultIdentity<ApplicationUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequiredLength = 4;
            //    //options.User.AllowedUserNameCharacters = null;
            //});
            //var key = Encoding.UTF8.GetBytes("1234567890123456");
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        ValidateIssuer = false,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateAudience = false,
            //        ClockSkew = TimeSpan.Zero


            //    };
            //});

            //services.AddSession(options =>
            //{
            //    options.IOTimeout = TimeSpan.FromMinutes(4);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
