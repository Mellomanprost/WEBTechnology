using Microsoft.AspNetCore.Identity;

namespace WEBTechnology.DAL.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public int Age { get; set; }

        public List<Role> Roles { get; set; } = new();
    }
}
