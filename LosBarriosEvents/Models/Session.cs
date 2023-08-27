using System.ComponentModel.DataAnnotations;
using LosBarriosEvents.Models.CustomDataAnnotations;

namespace LosBarriosEvents.Models
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; } //PK
        //SessionId
        //Session.cs
        public string Name { get; set; } = string.Empty;

        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$",
         ErrorMessage = "Time must be in HH:MM Format")]
        public string StartTime { get; set; } = string.Empty;
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$",
         ErrorMessage = "Time must be in HH:MMformat")]
        public string EndTime { get; set; } = string.Empty;

        //Navigation Properties
        public int EventId { get; set; }
        public Event? Event { get; set; }

        public List<SessionSpeaker>? SessionSpeakers {get; set;}


        public string? Duration
        {
            get
            {
                try
                {
                    return (DateTime.Parse(EndTime).Subtract(DateTime.Parse(StartTime))).ToString(@"hh\:mm");
                }
                catch
                {
                    return "Error";
                } 
            }
        }
    }
}