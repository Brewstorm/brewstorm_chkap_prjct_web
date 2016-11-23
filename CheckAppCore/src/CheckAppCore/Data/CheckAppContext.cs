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
        public DbSet<AgendaAppointment> AgendaAppointments { get; set; }
        public DbSet<AgendaSchedule> AgendaSchedules { get; set; }
        public DbSet<AgendaException> AgendaExceptions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UsersRoles { get; set; }

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
            
            modelBuilder.Entity<AgendaSchedule>().ToTable("AgendaSchedules")
                                                    .HasMany(o => o.AgendaExceptions)
                                                    .WithOne();

            modelBuilder.Entity<AgendaException>().HasOne(ae => ae.AgendaSchedule)
                                                   .WithMany();

            modelBuilder.Entity<AgendaAppointment>().HasOne(ae => ae.AgendaSchedule)
                                                    .WithMany(aS => aS.AgendaAppointments)
                                                   .HasForeignKey(aS => aS.AgendaScheduleID);

            modelBuilder.Entity<AgendaAppointment>().HasOne(ae => ae.User)
                                                    .WithMany(aS => aS.AgendaAppointments)
                                                   .HasForeignKey(aS => aS.UserID);

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<UserRole>()
                .HasKey(t => new { t.RoleID, t.UserID });

            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.Role)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pt => pt.RoleID);

            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(pt => pt.UserID);
        }
    }
}
