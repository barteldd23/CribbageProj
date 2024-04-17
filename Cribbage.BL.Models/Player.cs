namespace Cribbage.BL.Models
{
    public class Player : User
    {
        public List<Card> Hand { get; set; } = new List<Card>();
        public List<Card> PlayedCards { get; set; } = new List<Card>();
        public int HandPoints { get; set; } = 0;

        public int Score { get; set; } = 0;
        public bool SaidGo { get; set; } = false;


        public Player(User user)
        {
            Id = user.Id;
            Email = user.Email;
            DisplayName = user.DisplayName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Password = user.Password;
            GamesStarted = user.GamesStarted;
            GamesWon = user.GamesWon;
            GamesLost = user.GamesLost;
            WinStreak = user.WinStreak;
            AvgPtsPerGame = user.AvgPtsPerGame;
        }
    }

}
