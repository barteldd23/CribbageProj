namespace Cribbage.PL.Entities
{
    public class tblUserGame : IEntity
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public int PlayerScore { get; set; }
        public virtual tblGame Game { get; set; }
        public virtual tblUser User { get; set; }
    }
}
