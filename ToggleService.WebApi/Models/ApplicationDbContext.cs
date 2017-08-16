using Microsoft.EntityFrameworkCore;

namespace ToggleService.WebApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) { }
    }
}
