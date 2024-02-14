namespace Cribbage.BL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string LastFirstName { get { return LastName + ", " + FirstName; } }
        public string Password { get; set; }
        public int GamesStarted { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set;}
        public int WinStreak { get; set; }
        public double AvgPtsPerGame { get; set; }
        public double AvgHandScore { get; set; }
    }
}
