using Microsoft.EntityFrameworkCore;
using FlightReservationApp_f.Models;

namespace FlightReservationApp_f.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Flight> Flight { get; set; }
        public DbSet<Passenger> Passenger { get; set; }
        public DbSet<FlightPassenger> FlightPassenger { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Admin> Admin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Flight>()
                .Property(f => f.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<FlightPassenger>()
                .HasOne(fp => fp.Flight)
                .WithMany(f => f.FlightPassengers)
                .HasForeignKey(fp => fp.FlightId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FlightPassenger>()
                .HasOne(fp => fp.Passenger)
                .WithMany(p => p.FlightsPassenger)
                .HasForeignKey(fp => fp.PassengerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
