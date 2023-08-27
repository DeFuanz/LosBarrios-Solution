using System.ComponentModel.DataAnnotations;

namespace LosBarriosEvents.Models
{
    public class Volunteer
    {
        [Key]
        public int VolunteerId { get; set; } //PK

        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string emailAddress { get; set; } = string.Empty;
        public string volunteerTime { get; set; } = string.Empty;
        public string phoneNumber { get; set; } = string.Empty;
    }
}