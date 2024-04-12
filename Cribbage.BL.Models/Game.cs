using System.ComponentModel;

namespace Cribbage.BL.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid Winner { get; set; } 
        public DateTime Date { get; set; }
        public string? GameName { get; set; }
        public bool Complete { get; set; } = false;
    }
}
