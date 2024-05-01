using Cribbage.BL.Models;
using System.Numerics;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Cribbage.BL
{
    public class GameManager : GenericManager<tblGame>
    {
        public GameManager(DbContextOptions<CribbageEntities> options) : base(options) { }
        public GameManager(ILogger logger, DbContextOptions<CribbageEntities> options) : base(logger, options) { }
        #region DB Methods
        public int Insert(Game game, bool rollback = false)
        {
            try
            {
                int results;

                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGame newRow = new tblGame();

                    newRow.Id = Guid.NewGuid();
                    newRow.Winner = game.Winner;
                    newRow.Date = game.Date;
                    newRow.GameName = game.GameName;
                    newRow.Complete = game.Complete;

                    game.Id = newRow.Id;

                    dc.tblGames.Add(newRow);
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


        public int Update(Game game, bool rollback = false)
        {
            try
            {
                int results;

                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGame updateRow = dc.tblGames.FirstOrDefault(g => g.Id == game.Id);

                    if (updateRow != null)
                    {
                        updateRow.Winner = game.Winner;
                        updateRow.Date = game.Date;
                        updateRow.GameName = game.GameName;
                        updateRow.Complete = game.Complete;

                        dc.tblGames.Update(updateRow);

                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row not found.");
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

                    tblGame deleteRow = dc.tblGames.FirstOrDefault(g => g.Id == id);

                    if (deleteRow != null)
                    {
                        // remove the row from tblUserGame
                        var userGame = dc.tblUserGames.Where(g => g.GameId == id);
                        dc.tblUserGames.RemoveRange(userGame);

                        // remove the game from tblGame
                        dc.tblGames.Remove(deleteRow);

                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row not found.");
                    }
                }
                return results;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Game> Load()
        {
            try
            {
                List<Game> games = new List<Game>();

                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    games = (from g in dc.tblGames
                             select new Game
                             {
                                 Id = g.Id,
                                 Winner = g.Winner,
                                 Date = g.Date,
                                 GameName = g.GameName,
                                 Complete = g.Complete
                             }
                             )
                             .OrderBy(g => g.GameName)
                             .ToList();
                }
                return games;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Game LoadById(Guid id)
        {
            try
            {
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    tblGame row = dc.tblGames.FirstOrDefault(g => g.Id == id);

                    if (row != null)
                    {
                        Game game = new Game
                        {
                            Id = row.Id,
                            Winner = row.Winner,
                            Date = row.Date,
                            GameName = row.GameName,
                            Complete = row.Complete
                        };
                        return game;
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
        public Game GetAvailableGame()
        {
            try
            {
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    tblGame row = dc.tblGames.Where(g => g.tblUserGames.Count() == 0).OrderBy(g => g.Date).FirstOrDefault();

                    if (row != null)
                    {
                        Game game = new Game
                        {
                            Id = row.Id,
                            Winner = row.Winner,
                            Date = row.Date,
                            GameName = row.GameName,
                            Complete = row.Complete
                        };
                        return game;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion


    }
}