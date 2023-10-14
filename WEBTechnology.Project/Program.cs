using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WEBTechnology.BLL.Services;
using WEBTechnology.BLL.Services.IServices;
using WEBTechnology.DAL.Models;

namespace WEBTechnology.Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<WebDbContext>(options => options.UseSqlServer(connection))
                .AddIdentity<User, Role>(opts =>
                {
                    opts.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<WebDbContext>();

            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            builder.Services.AddSingleton(mapper)
                .AddTransient<IUserService, UserService>()
                .AddTransient<IHomeService, HomeService>()
                .AddTransient<IRoleService, RoleService>()
                .AddControllersWithViews();




            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}