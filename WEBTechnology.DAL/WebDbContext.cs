using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WEBTechnology.DAL.Models;


namespace WEBTechnology.Project
{
    public class WebDbContext : IdentityDbContext<User>
    {
        ///// Ссылка на таблицу Role
        //public DbSet<Role>? Roles { get; set; }

        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
