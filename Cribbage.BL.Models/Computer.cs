using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage.BL.Models
{
    public class Computer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> PlayedCards { get; set; }
        public int HandPoints { get; set; }
    }
}
