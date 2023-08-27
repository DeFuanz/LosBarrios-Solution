using System.ComponentModel.DataAnnotations;

namespace LosBarriosEvents.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; } //PK
        public string roomNumber { get; set; } = string.Empty;
    }

}