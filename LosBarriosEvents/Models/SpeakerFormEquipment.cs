namespace LosBarriosEvents.Models;

public class SpeakerFormEquipment
{
    //Composite Key
    public int SpeakerFormId { get; set; }
    public int EquipmentId { get; set; }

    //Navigation Properties
    public SpeakerForm? SpeakerForm { get; set; }
    public Equipment? Equipment { get; set; }
}