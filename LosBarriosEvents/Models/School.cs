using System.ComponentModel.DataAnnotations;

namespace LosBarriosEvents.Models
{
    public class School
    {
        [Key]
        public int SchoolId { get; set; } //PK
        public string Name { get; set; }
        public string District { get; set; }
        public string City { get; set; }
    }

}