using Cribbage.BL.Models;

namespace Cribbage.BL
{
    public class PlayerManager : GenericManager<tblUser>
    {
        public PlayerManager(DbContextOptions<CribbageEntities> options) : base(options)
        {
        }

        public int Insert(Player player, bool rollback = false)
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


        public int Update(Player player, bool rollback = false)
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

        public int Delete(Player player, bool rollback = false)
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

        public int Load(Player player, bool rollback = false)
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

        public int LoadById(Player player, bool rollback = false)
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
