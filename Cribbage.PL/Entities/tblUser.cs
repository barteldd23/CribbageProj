using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage.PL.Entities
{
    public class tblUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int GamesStarted { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public double AvgPtsPerGame { get; set; }
        public int WinStreak { get; set; }
        public virtual ICollection<tblUserGame> tblUserGames { get; set; }
    }
}
