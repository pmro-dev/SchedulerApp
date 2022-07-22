using Microsoft.AspNetCore.Identity;

namespace SchedulerApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
