namespace Cribbage.BL.Models
{
    public class UserGame
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public int PlayerScore { get; set; }
    }
}
