using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage.BL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string LastFirstName { get { return LastName + ", " + FirstName; } }
        public string Password { get; set; }
        public int GamesInitiated { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set;}
        public int WinStreak { get; set; }
        public double AvgPtsPerGame { get; set; }
        public double AvgHandScore { get; set; }

    }
}
