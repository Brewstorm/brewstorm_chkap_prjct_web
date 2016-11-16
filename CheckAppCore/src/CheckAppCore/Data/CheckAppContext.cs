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
        public DbSet<PersonalInfo> PersonalInfo { get; set; }
        public DbSet<Agenda> Agenda { get; set; }
        public DbSet<AgendaSchedule> AgendaSchedules { get; set; }
        public DbSet<AgendaException> AgendaExceptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentType>().ToTable("AppointmentType");
            modelBuilder.Entity<Professional>().ToTable("Professionals")
                                               .HasOne(p => p.PersonalInfo)
                                               .WithOne();

            modelBuilder.Entity<PersonalInfo>().ToTable("PersonalInfo");
            
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

            modelBuilder.Entity<Agenda>().ToTable("Agenda")
                                         .HasOne(ag => ag.Professional)
                                         .WithOne();

            modelBuilder.Entity<Agenda>().HasOne(ag => ag.AppointmentType)
                                         .WithMany();

            modelBuilder.Entity<Agenda>().HasOne(ag => ag.AgendaSchedule)
                                         .WithOne(ag => ag.Agenda)
                                         .HasForeignKey<AgendaSchedule>(a => a.AgendaID);

            //modelBuilder.Entity<AgendaSchedule>().ToTable("AgendaSchedules")
            //                                        .HasOne(aS => aS.Agenda)
            //                                        .WithOne();

            modelBuilder.Entity<AgendaSchedule>().ToTable("AgendaSchedules")
                                                    .HasMany(o => o.AgendaExceptions)
                                                    .WithOne();

            modelBuilder.Entity<AgendaException>().HasOne(ae => ae.AgendaSchedule)
                                                   .WithMany();

        }
    }
}
