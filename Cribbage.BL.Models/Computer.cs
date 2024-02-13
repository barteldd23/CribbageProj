namespace Cribbage.BL.Models
{
    public class Computer
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> PlayedCards { get; set; }
        public int HandPoints { get; set; }
    }
}
