using Microsoft.EntityFrameworkCore;
using ttgapp.Models;

namespace ttgapp.Dal
{
    public class TTGContext : DbContext
    {
        public TTGContext(DbContextOptions<TTGContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Slider>().ToTable("Slider");
            modelBuilder.Entity<TouristPlaceType>().ToTable("TouristPlaceType");
            modelBuilder.Entity<TouristPlace>().ToTable("TouristPlace");
            modelBuilder.Entity<Package>().ToTable("Package");
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Role> roles { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Slider> sliders { get; set; }
        public DbSet<TouristPlaceType> touristPlaceTypes { get; set; }
        public DbSet<TouristPlace> touristPlaces { get; set; }
        public DbSet<Package> packages { get; set; }
        public DbSet<ttgapp.Models.UserLogin> UserLogin { get; set; } = default!;
        public DbSet<ttgapp.Models.ImageGallery> ImageGallery { get; set; } = default!;

    }
}
