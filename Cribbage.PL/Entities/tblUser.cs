namespace Cribbage.PL.Entities
{
    public class tblUser : IEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int GamesStarted { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public int WinStreak { get; set; }
        public double AvgPtsPerGame { get; set; }
        //public double AvgHandScore { get; set; }
        public virtual ICollection<tblUserGame> tblUserGames { get; set; }
    }
}
