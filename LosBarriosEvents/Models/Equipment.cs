using System.ComponentModel.DataAnnotations;

namespace LosBarriosEvents.Models;

public class Equipment
{
    [Key]
    public int EquipmentId { get; set; }
    public string Name { get; set; } = string.Empty;

    //Navigation Properties
    public List<SpeakerFormEquipment>? SpeakerFormEquipment { get; set; }
}