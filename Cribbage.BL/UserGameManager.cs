using Cribbage.BL.Models;

namespace Cribbage.BL
{
    public class UserGameManager : GenericManager<tblUserGame>
    {
        public UserGameManager(DbContextOptions<CribbageEntities> options) : base(options)
        {
        }

        public int Insert(UserGame userGame, bool rollback = false)
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
                    row.GameId = userGame.GameId;
                    row.PlayerId = userGame.PlayerId;
                    row.PlayerScore = userGame.PlayerScore;

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

        public List<Guid> GetGames(Guid playerId)
        {
            try
            {
                List<Guid> list = new List<Guid>();
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    tblUserGame entity = dc.tblUserGames.Where(ug => ug.PlayerId == playerId).FirstOrDefault();
                    if (entity != null)
                    {
                        List<tblUserGame> entities = dc.tblUserGames.Where(ug => ug.PlayerId == playerId).ToList();
                        foreach (tblUserGame userGame in entities)
                        {
                            list.Add(userGame.GameId);
                        }
                        return list;
                    }
                    else
                    {
                        // no games for the user
                        return list;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UserGame> GetGamesVsComputer(Guid playerId)
        {
            try
            {
                List<UserGame> savedGamesVsComputer = new List<UserGame>();

                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    savedGamesVsComputer = (from ug in dc.tblUserGames
                                            join g in dc.tblGames on ug.GameId equals g.Id
                                            where ug.PlayerId == playerId && g.GameName.Contains("Computer") && !g.Complete
                                            select new UserGame
                                            {
                                                Id = ug.Id,
                                                GameId = ug.GameId,
                                                PlayerId = playerId,
                                                PlayerScore = ug.PlayerScore
                                            }).ToList();

                    return savedGamesVsComputer;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public float CalculateAvgPtsPerGame(User user)
        {
            try
            {
                int numRows = 0;
                int points = 0;
                float avgPtsPerGame = 0;

                List<UserGame> games = new List<UserGame>();

                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    games = (from ug in dc.tblUserGames
                             join g in dc.tblGames on ug.GameId equals g.Id
                             where ug.PlayerId == user.Id && g.Complete 
                             select new UserGame
                             {
                                 Id = ug.Id,
                                 GameId = ug.GameId,
                                 PlayerId = ug.PlayerId,
                                 PlayerScore = ug.PlayerScore
                             }).ToList();

                    foreach(UserGame game in games) 
                    {
                        numRows++;
                        points += game.PlayerScore;
                    }

                    avgPtsPerGame = points / numRows;

                    return avgPtsPerGame;
                }
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
        public int Update(CribbageGame cribbageGame, bool rollback = false)
        {
            try
            {
                int results;
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    //string gameId = cribbageGame.Id.ToString();
                    //string p1Id = cribbageGame.Player_1.Id.ToString();
                    //string p2Id = cribbageGame.Player_2.Id.ToString();
                    tblUserGame updateRow1 = dc.tblUserGames.FirstOrDefault(r => r.GameId == cribbageGame.Id && r.PlayerId == cribbageGame.Player_1.Id);
                    tblUserGame updateRow2 = updateRow2 = dc.tblUserGames.FirstOrDefault(r => r.GameId == cribbageGame.Id && r.PlayerId == cribbageGame.Player_2.Id);
                    

                    if (updateRow1 != null)
                    {
                        updateRow1.PlayerScore = cribbageGame.Team1_Score;
                        updateRow2.PlayerScore = cribbageGame.Team2_Score;

                        dc.tblUserGames.Update(updateRow1);
                        dc.tblUserGames.Update(updateRow2);

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
        public UserGame LoadByIds(Guid userId, Guid gameId)
        {
            try
            {
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    tblUserGame row = dc.tblUserGames.FirstOrDefault(ug => ug.PlayerId == userId && ug.GameId == gameId);

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
