using Microsoft.EntityFrameworkCore;
using Portfolio.Models.Model;

namespace Portfolio.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
