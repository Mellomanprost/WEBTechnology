using AutoMapper;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WEBTechnology.API1.Contracts;
using WEBTechnology.Project.Models;
using WEBTechnology.Project.Services.IServices;
using WEBTechnology.Project.Services;

namespace WEBTechnology.API1
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
                    Description = "Test Task",
                });
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line

                //var basePath = AppContext.BaseDirectory;
                //var xmlPath = Path.Combine(basePath, "WEBTechnology.API.xml");
                //options.IncludeXmlComments(xmlPath);
            });

            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            var assembly = Assembly.GetAssembly(typeof(MappingProfile));
            builder.Services.AddAutoMapper(assembly);

            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DbContext>(options => options.UseSqlServer(connection))
                .AddIdentity<User, Role>(opts =>
                {
                    opts.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<DbContext>();

            // Services AddSingletons,Transient
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEBTechnology v1"));
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}