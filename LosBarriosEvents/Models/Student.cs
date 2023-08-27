using System.ComponentModel.DataAnnotations;

namespace LosBarriosEvents.Models
{
    public class Student
    {
        [Key]
        public string? StudentId { get; set; } //PK

        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string gender { get; set; } = string.Empty;
        public string ethnicity { get; set; } = string.Empty;

        //Navigation Properties
        public List<EventStudent> EventStudents { get; set; } = default!;
    }

}