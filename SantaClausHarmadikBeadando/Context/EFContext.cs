using Microsoft.EntityFrameworkCore;
using SantaClausHarmadikBeadando.Models;

namespace SantaClausHarmadikBeadando.Context
{
    public class EFContext : DbContext
    {

        public DbSet<SantaClaus> Wishes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=.//DB//santaclaus.db");
        }

        /*
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SantaClaus>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }
        */

    }
}
