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
        public List<Card> PlayerHand { get; set; }
        public List<Card> PlayedCards { get; set; }

    }
}
