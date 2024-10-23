using System.ComponentModel.DataAnnotations;

namespace StudentAttendance.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Student Name")]
        public string StudentName { get; set;} = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "Enrollment Number")]
        public string EnrollmentNumber { get; set; } = string.Empty;

        public ICollection<Attendance>? Attendances { get; set; }
    }
}
