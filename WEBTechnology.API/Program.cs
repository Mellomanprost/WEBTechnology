using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WEBTechnology.Project;
using MappingProfile = WEBTechnology.API.Contracts.MappingProfile;
using Microsoft.OpenApi.Models;
using WEBTechnology.BLL.Services;
using WEBTechnology.BLL.Services.IServices;
using WEBTechnology.DAL.Models;

namespace WEBTechnology.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WEBTechnology API",
                    Description = "created by Mellomanprost",
                });
                var basePath = AppContext.BaseDirectory;

                var xmlPath = Path.Combine(basePath, "WEBTechnology.API.xml");
                options.IncludeXmlComments(xmlPath);
            });


            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            var assembly = Assembly.GetAssembly(typeof(MappingProfile));

            builder.Services.AddAutoMapper(assembly);

            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<WebDbContext>(options => options.UseSqlServer(connection))
                .AddIdentity<User, Role>(opts =>
                {
                    opts.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<WebDbContext>();

            builder.Services
                .AddSingleton(mapper)
                .AddTransient<IUserService, UserService>()
                .AddTransient<IRoleService, RoleService>();

            //builder.Services.AddAuthentication(optionts => optionts.DefaultScheme = "Cookies")
            //    .AddCookie("Cookies", options =>
            //    {
            //        options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
            //        {
            //            OnRedirectToLogin = redirectContext =>
            //            {
            //                redirectContext.HttpContext.Response.StatusCode = 401;
            //                return Task.CompletedTask;
            //            }
            //        };
            //    });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}