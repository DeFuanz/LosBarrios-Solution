using System.ComponentModel.DataAnnotations;

namespace LosBarriosEvents.Models
{
    public class VolunteerWork
    {
        [Key]
        public int WorkId { get; set; } //PK

        public string Event { get; set; } = string.Empty;
    }
}