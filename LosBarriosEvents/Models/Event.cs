using System.ComponentModel.DataAnnotations;

namespace LosBarriosEvents.Models
{
    public class Event
    {
        public int EventId { get; set; } //PK
        public string Name { get; set; } = string.Empty;

        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$",
         ErrorMessage = "Time must be in HH:MM Format")]
        public string Time { get; set; } = string.Empty;

        //Date regex actually validates yyyy-mm-dd even though date picker shows mm-dd-yyyy due to conversion that happens in posting
        [RegularExpression(@"^([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))",
         ErrorMessage = "Date Must be in MM-DD-YYYY Format")]
        public string Date { get; set; } = string.Empty;

        //Navigation Properties
        public List<EventStudent>? EventStudents { get; set; }

        public List<Session>? Sessions { get; set; }
        
    }
}