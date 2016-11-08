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
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<ProfessionalAppointmentType> ProfessionalsAppointmentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentType>().ToTable("AppointmentType");
            modelBuilder.Entity<Professional>().ToTable("Professionals");

            modelBuilder.Entity<ProfessionalAppointmentType>()
                .HasKey(t => new { t.ProfessionalId, t.AppointmentTypeId });

            modelBuilder.Entity<ProfessionalAppointmentType>()
                .HasOne(pt => pt.Professional)
                .WithMany(p => p.ProfessionalAppointmentTypes)
                .HasForeignKey(pt => pt.ProfessionalId);

            modelBuilder.Entity<ProfessionalAppointmentType>()
                .HasOne(pt => pt.AppointmentType)
                .WithMany(t => t.ProfessionalAppointmentTypes)
                .HasForeignKey(pt => pt.AppointmentTypeId);
        }
    }
}
