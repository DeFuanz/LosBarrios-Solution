namespace LosBarriosEvents.Models;

public class SessionSpeaker
{
    //PK-Composite key
    public int SessionId { get; set; }
    public string SpeakerId { get; set; } = string.Empty;

    //Extra properties
    public int? FormIdAssigned { get; set; }

    //navigaitonal properties
    public Session Session { get; set; } = default!;
    public Speaker Speaker { get; set; } = default!;
}