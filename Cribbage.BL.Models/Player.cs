namespace Cribbage.BL.Models
{
    public class Player : User
    {
        public List<Card> Hand { get; set; } = new List<Card>();
        public List<Card> PlayedCards { get; set; } = new List<Card>();
        public int HandPoints { get; set; }

        public int Score { get; set; }
        public bool SaidGo { get; set; }
    }
}
