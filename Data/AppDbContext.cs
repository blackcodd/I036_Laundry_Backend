using Microsoft.EntityFrameworkCore;
using TechLaundry.Models; // Assuming you have a Models namespace for your entity classes   

namespace TechLaundry.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // Define your DbSets here, for example:
         public DbSet<User> Users { get; set; }
    }
}
