using Microsoft.EntityFrameworkCore;
using Trail_A.Models;

namespace Trail_A.Database_Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<User> users { get; set; }
        public DbSet<Ride> rides { get; set; }
        public DbSet<VehicleType> vehicleTypes { get; set; }
        public DbSet<Driver> drivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //(User -> Driver) One To Many
            modelBuilder.Entity<Driver>()
                .HasOne(x => x.user)
                .WithMany(z => z.drivers)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            //(User -> VehicleType) One To Many
            modelBuilder.Entity<VehicleType>()
                .HasOne(x => x.user)
                .WithMany(z => z.vehicleTypes)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            //(User -> Ride) One To Many
            modelBuilder.Entity<Ride>()
                .HasOne(x => x.user)
                .WithMany(z => z.rides)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //(Drive -> Ride) One To Many
            modelBuilder.Entity<Ride>()
                .HasOne(x=>x.driver)
                .WithMany(x=>x.rides)
                .HasForeignKey(x=>x.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            //(VehicleType -> Ride) One To Many
            modelBuilder.Entity<Ride>()
                .HasOne(x=>x.vehicleType)
                .WithMany(z=>z.Rides)
                .HasForeignKey(x=>x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
