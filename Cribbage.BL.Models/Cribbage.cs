using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage.BL.Models
{
    public class Cribbage
    {
        public int Id { get; set; }
        public int Dealer { get; set; } = 1;
        public Deck Deck { get; set; }
        public List<Card> Crib { get; set; }
        public Card CutCard { get; set; }

        [DisplayName("Team 1 Score")] 
        public int Team1_Score { get; set; }
        [DisplayName("Team 2 Score")]
        public int Team2_Score { get; set;}

    }
}
