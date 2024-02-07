using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage.PL.Entities
{
    public class tblUserGame
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public int PlayerScore { get; set; }
        public virtual tblGame Game { get; set; }
        public virtual tblUser User { get; set; }
    }
}
