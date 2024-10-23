using Microsoft.EntityFrameworkCore;
using StudentAttendance.Models;

namespace StudentAttendance.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the relationship between Student and Attendance
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Student) // Each Attendance has one Student
                .WithMany(s => s.Attendances) // Each Student has many Attendances
                .HasForeignKey(a => a.StudentId) // Foreign key in Attendance table
                .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete
        }

    }
}
