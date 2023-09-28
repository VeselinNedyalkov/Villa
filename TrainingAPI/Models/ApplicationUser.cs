using Microsoft.AspNetCore.Identity;

namespace TrainingAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
