using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage.PL.Entities
{
    public class tblGames
    {
        public Guid Id { get; set; }
        public Guid Player_1_Id { get; set; }
        public Guid Player_2_Id { get; set; }
        public int Player_1_Score { get; set; }
        public int Player_2_Score { get; set; }
        public Guid Winner { get; set; }
        public DateTime Date { get; set; }
    }
}
