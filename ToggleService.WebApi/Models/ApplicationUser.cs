using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ToggleService.WebApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string AppKeyName { get; set; }
    }
   
}
