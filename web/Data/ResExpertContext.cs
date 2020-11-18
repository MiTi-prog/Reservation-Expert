using web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public class ResExpertContext : DbContext
    {
        public ResExpertContext(DbContextOptions<ResExpertContext> options) : base(options)
        {
        }   
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Table> Tables { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // preimenujemo iz Guests v Guest nazaj
            modelBuilder.Entity<Guest>().ToTable("Guest");
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<Restaurant>().ToTable("Restaurant");
            modelBuilder.Entity<Table>().ToTable("Table");

        }
    


    }
}