using System;
using System.ComponentModel.DataAnnotations;

namespace LosBarriosEvents.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        public int Rating { get; set; }

        public string? CreatedOn { get; set; }

        public int SessionId { get; set; }

        public Session? Session { get; set; }

        public int OverallLectureRating { get; set; }

        public int SpeakerRating { get; set; }

        public int ContentRating { get; set; }

        public int FacilityRating { get; set; }

        [Required]
        public string? Comment { get; set; }
    }
}
