using Microsoft.EntityFrameworkCore;
using JewelleryVerificationProject.Models;

namespace JewelleryVerificationProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Jewellery> Jewellery { get; set; }
    }
}
