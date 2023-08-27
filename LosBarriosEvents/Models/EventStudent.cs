namespace LosBarriosEvents.Models;

public class EventStudent
{
    //PK - Composite key
    public int EventId { get; set; }
    public string? StudentId { get; set; }

    //Navigation Properties
    public Event Event { get; set; } = default!;
    public Student Student { get; set; } = default!;
}