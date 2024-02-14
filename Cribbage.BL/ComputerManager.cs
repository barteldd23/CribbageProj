using Cribbage.BL.Models;

namespace Cribbage.BL
{
    public class ComputerManager : GenericManager<tblUser>
    {
        public ComputerManager(DbContextOptions<CribbageEntities> options) : base(options)
        {
        }

        public int Insert(Computer computer, bool rollback = false)
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


        public int Update(Computer computer, bool rollback = false)
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

        public int Delete(Computer computer, bool rollback = false)
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

        public int Load(Computer computer, bool rollback = false)
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

        public int LoadById(Computer computer, bool rollback = false)
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
