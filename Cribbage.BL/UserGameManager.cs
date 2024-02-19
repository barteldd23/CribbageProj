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


        public int Update(UserGame userGame, bool rollback = false)
        {
            try
            {
                int results;
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUserGame updateRow = dc.tblUserGames.FirstOrDefault(r => r.Id == userGame.Id);

                   if (updateRow != null)
                    {
                        updateRow.GameId = userGame.GameId;
                        updateRow.PlayerId = userGame.PlayerId;
                        updateRow.PlayerScore = userGame.PlayerScore;

                        dc.tblUserGames.Update(updateRow);

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

        public UserGame LoadById(Guid id)
        {
            try
            {
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    tblUserGame row = dc.tblUserGames.FirstOrDefault(ug => ug.Id == id);

                    if (row != null)
                    {
                        UserGame userGame = new UserGame
                        {
                            Id = row.Id,
                            GameId = row.GameId,
                            PlayerId = row.PlayerId,
                            PlayerScore = row.PlayerScore
                        };
                        return userGame;
                    }
                    else
                    {
                        throw new Exception("Row not found");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UserGame> Load()
        {
            try
            {
                List<UserGame> userGames = new List<UserGame>();

                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    userGames = (from ug in dc.tblUserGames
                                 select new UserGame
                                 {
                                     Id = ug.Id,
                                     GameId = ug.GameId,
                                     PlayerId = ug.PlayerId,
                                     PlayerScore = ug.PlayerScore
                                 }
                                 )
                                 .OrderBy(ug => ug.Id)
                                 .ToList();
                }
                        return userGames;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
