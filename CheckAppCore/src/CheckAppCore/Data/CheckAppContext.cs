using CheckAppCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckAppCore.Data
{
    public class CheckAppContext : DbContext
    {
        public CheckAppContext(DbContextOptions<CheckAppContext> options) : base(options)
        {
        }

        public DbSet<AppointmentType> AppointmentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentType>().ToTable("AppointmentType");;
        }
    }
}
