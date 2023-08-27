using System.ComponentModel.DataAnnotations;

namespace LosBarriosEvents.Models
{
    public class Speaker
    {
        [Key]
        public string SpeakerId { get; set; } = default!;//PK

        public string Name { get; set; } = String.Empty;
        public string email { get; set; } = String.Empty;
        public string phoneNumber { get; set; } = String.Empty;

        public string BIO { get; set; } = String.Empty;

        //Navigation Properties
        public List<SpeakerForm>? SpeakerForms { get; set; }

        public List<SessionSpeaker>? SessionSpeakers { get; set; }
    }
}