using System.ComponentModel.DataAnnotations;

namespace LosBarriosEvents.Models
{
    public class Facility
    { 
        [Key]
        public int FacilityId { get; set; } //PK

        public string BuildingName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}