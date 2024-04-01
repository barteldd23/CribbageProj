using Microsoft.EntityFrameworkCore.Migrations;

namespace Cribbage.PL.Entities
{
    public class spGetMostWinsResult : IEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string LastFirstName { get; set; }
        public string Password { get; set; }
        public int GamesStarted { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public int WinStreak { get; set; }
        public double AvgPtsPerGame { get; set; }
        public string SortField { get { return DisplayName; } }
    }
}