using System.ComponentModel.DataAnnotations;

namespace StudentAttendance.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsPresent { get; set; }

        public virtual Student Student { get; set; } = null!;
    }
}
