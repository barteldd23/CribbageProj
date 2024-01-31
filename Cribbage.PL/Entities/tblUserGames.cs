using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage.PL.Entities
{
    public class tblUserGames
    {
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
    }
}
