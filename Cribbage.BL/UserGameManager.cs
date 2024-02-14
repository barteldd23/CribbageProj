using Cribbage.BL.Models;

namespace Cribbage.BL
{
    public class UserGameManager : GenericManager<tblUserGame>
    {
        public UserGameManager(DbContextOptions<CribbageEntities> options) : base(options)
        {
        }

        public int Insert(Guid gameId, Guid playerId, int playerScore, bool rollback = false)
        {
            try
            {
                int results;
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblUserGame row = new tblUserGame();

                    row.Id = Guid.NewGuid();
                    row.GameId = gameId;
                    row.PlayerId = playerId;
                    row.PlayerScore = playerScore;

                    dc.tblUserGames.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception e)
            {

                throw e;
            }

        }


        public int Update(Guid gameId, Guid playerId, int playerScore, bool rollback = false)
        {
            try
            {
                int results;
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUserGame row = dc.tblUserGames.FirstOrDefault(r => r.GameId == gameId && r.PlayerId == playerId && r.PlayerScore == playerScore);

                   if (row != null)
                    {
                        row.GameId = gameId;
                        row.PlayerId = playerId;
                        row.PlayerScore = playerScore;

                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                   else
                    {
                        throw new Exception("Row not found");
                    }
                }
                return results;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public int Delete(Guid gameId, Guid playerId, int playerScore, bool rollback = false)
        {
            try
            {
                int results;
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUserGame row = dc.tblUserGames.FirstOrDefault(r => r.GameId == gameId && r.PlayerId == playerId && r.PlayerScore == playerScore);

                    if (row != null)
                    {
                        dc.tblUserGames.Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row not found");
                    }
                }
                    return results;
                }
            catch (Exception e)
            {

                throw e;
            }

        }

        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                int results;
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUserGame row = dc.tblUserGames.FirstOrDefault(r => r.Id == id);

                    if (row != null)
                    {
                        dc.tblUserGames.Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row not found");
                    }
                }
                return results;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
