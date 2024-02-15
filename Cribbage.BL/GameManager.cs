using Cribbage.BL.Models;

namespace Cribbage.BL
{
    public class GameManager : GenericManager<tblGame>
    {
        public GameManager(DbContextOptions<CribbageEntities> options) : base(options)
        {
        }

        public int Insert(Game game, bool rollback = false)
        {
            try
            {
                //int results;

                //using (CribbageEntities dc = new CribbageEntities(options))
                //{
                //    IDbContextTransaction transaction = null;
                //    if (rollback) transaction = dc.Database.BeginTransaction();

                //    tblGame newRow = new tblGame();

                //    newRow.Id = Guid.NewGuid();
                //    newRow.Winner = game.Winner;

                //}

                return 1;
            }
            catch (Exception e)
            {

                throw e;
            }

        }


        public int Update(Game cribbage, bool rollback = false)
        {
            try
            {
                return 1;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public int Delete(Game cribbage, bool rollback = false)
        {
            try
            {
                return 1;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public int Load(Game cribbage, bool rollback = false)
        {
            try
            {
                return 1;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public int LoadById(Game cribbage, bool rollback = false)
        {
            try
            {
                return 1;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
