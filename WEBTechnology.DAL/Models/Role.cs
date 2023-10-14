using Microsoft.AspNetCore.Identity;

namespace WEBTechnology.DAL.Models
{
    public class Role : IdentityRole
    {
        public string? Description { get; set; }

        public List<User> Users { get; set; } = new();
    }
}
