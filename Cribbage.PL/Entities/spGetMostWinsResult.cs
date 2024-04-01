using Microsoft.EntityFrameworkCore.Migrations;

namespace Cribbage.PL.Entities
{
    public class spGetMostWinsResult : IEntity
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public int GamesWon { get; set; }
        public string SortField { get { return GamesWon.ToString(); } }
    }
}