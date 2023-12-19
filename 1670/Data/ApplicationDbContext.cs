using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using _1670.Models;

namespace _1670.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<_1670.Models.Product> Product { get; set; } = default!;

        public DbSet<_1670.Models.Category> Category { get; set; } = default;

        public DbSet<_1670.Models.Order> Order { get; set; } = default!;
    }
}