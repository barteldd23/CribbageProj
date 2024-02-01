using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage.BL.Models
{
    public class Player : User
    {
        public List<Card> Hand { get; set; }
        public List<Card> PlayedCards { get; set; }
        public int HandPoints { get; set; }
        public int Score { get; set; }
    }
}
