using System.ComponentModel.DataAnnotations;

namespace LosBarriosEvents.Models;

public class SpeakerForm
{
    [Key]
    public int SpeakerFormId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;

    //Array for keywords
    public string[]? Keywords { get; set; }

    public FormStatus Status { get; set; } = FormStatus.Pending;

    //Foreign Keys
    public string? SpeakerId { get; set; }
    //Navigation Properties
    public Speaker? Speaker { get; set; }
    public List<SpeakerFormEquipment>? SpeakerFormEquipment { get; set; }
}

public enum FormStatus
{
    Pending,
    Approved,
    Rejected
}