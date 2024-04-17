namespace Cribbage.BL.Models
{
    public class UserGame
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public int PlayerScore { get; set; }

        public UserGame(Guid gameId, Guid playerId, int playerScore)
        {
            Id = Guid.NewGuid();
            GameId = gameId;
            PlayerId = playerId;
            PlayerScore = playerScore;
        }
        public UserGame()
        {
        }
        public UserGame(Guid id, Guid gameId, Guid playerId, int playerScore)
        {
            Id = id;
            GameId = gameId;
            PlayerId = playerId;
            PlayerScore = playerScore;
        }
    }
}
