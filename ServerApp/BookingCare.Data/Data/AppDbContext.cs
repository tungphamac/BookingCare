using BookingCare.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BookingCare.Data.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình User - Doctor (1-1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Doctor)
                .WithOne(d => d.User)
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa User thì xóa Doctor

            // Cấu hình User - Patient (1-1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Patient)
                .WithOne(p => p.User)
                .HasForeignKey<Patient>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa User thì xóa Patient

            // Cấu hình Patient - Appointments (1-nhiều)
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa Patient nếu có Appointments

            // Cấu hình Doctor - Appointments (1-nhiều)
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa Doctor nếu có Appointments

            // Cấu hình Doctor - Schedules (1-nhiều)
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Schedules)
                .WithOne(s => s.Doctor)
                .HasForeignKey(s => s.DoctorId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Doctor thì xóa Schedules

            // Cấu hình Appointment - Schedule (1-1 từ Appointment)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Schedule)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa Schedule nếu có Appointment

            // Cấu hình Appointment - Clinic (1-nhiều từ Clinic)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Clinic)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ClinicId)
                .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa Clinic nếu có Appointment

            // Cấu hình Appointment - Feedback (1-1)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Feedback)
                .WithOne(f => f.Appointment)
                .HasForeignKey<Feedback>(f => f.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Appointment thì xóa Feedback

            // Cấu hình Clinic - Doctors (1-nhiều)
            modelBuilder.Entity<Clinic>()
                .HasMany(c => c.Doctors)
                .WithOne(d => d.Clinic)
                .HasForeignKey(d => d.ClinicId)
                .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa Clinic nếu có Doctors

            // Cấu hình Clinic - Appointments (1-nhiều)
            modelBuilder.Entity<Clinic>()
                .HasMany(c => c.Appointments)
                .WithOne(a => a.Clinic)
                .HasForeignKey(a => a.ClinicId)
                .OnDelete(DeleteBehavior.Restrict); // Đã cấu hình ở trên, giữ nhất quán

            // Cấu hình Specialization - Doctors (1-nhiều)
            modelBuilder.Entity<Specialization>()
                .HasMany(s => s.Doctors)
                .WithOne(d => d.Specialization)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.Restrict); // Ngăn xóa Specialization nếu có Doctors

            // Cấu hình Appointment - MedicalRecord (1-1)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.MedicalRecord)
                .WithOne(m => m.Appointment)
                .HasForeignKey<MedicalRecord>(m => m.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Appointment thì xóa MedicalRecord

            SeedData.Seed(modelBuilder);
        }
    }
}
