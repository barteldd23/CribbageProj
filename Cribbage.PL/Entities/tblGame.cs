namespace Cribbage.PL.Entities
{
    public class tblGame : IEntity
    {
        public Guid Id { get; set; }
        public Guid Winner { get; set; }
        public DateTime Date { get; set; }
        public string? GameName { get; set; }
        public bool Complete { get; set; } // bit vs bool
        public virtual ICollection<tblUserGame> tblUserGames { get; set; }
        public string SortField { get { return GameName; } }
    }
}
