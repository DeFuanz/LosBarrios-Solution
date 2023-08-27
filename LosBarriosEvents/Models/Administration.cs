using System.ComponentModel.DataAnnotations;

namespace LosBarriosEvents.Models
{
    public class Administration
    {
        [Key]
        public int AdminId { get; set; } //PK

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}