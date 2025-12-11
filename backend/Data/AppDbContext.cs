using CafeRadar.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeRadar.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cafe> Cafes => Set<Cafe>();
        public DbSet<Measurement> Measurements => Set<Measurement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cafe>()
                .HasMany(x => x.Measurements)
                .WithOne(m => m.Cafe)
                .HasForeignKey(m => m.CafeId)
                .OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<Cafe>().HasData(
                new Cafe { Id = 1, Name = "Mood Bar - Night & Lounge Bar", Address = "Lacina 5, Mostar 88000 Bosnia and Herzegovina" },
                new Cafe { Id = 2, Name = "Caffe Pizzeria Urban Forest", Address = "Zalik 12, Mostar 88000 Bosnia and Herzegovina" },
                new Cafe { Id = 3, Name = "Fabrika Coffee Mostar", Address = "Fejiceva Bb, Mostar 88000 Bosnia and Herzegovina" }
            );
        }
    
    }
}
