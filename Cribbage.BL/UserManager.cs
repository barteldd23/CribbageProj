using Cribbage.BL.Models;

namespace Cribbage.BL
{
    public class UserManager : GenericManager<tblUser>
    {
        public UserManager(DbContextOptions<CribbageEntities> options) : base(options)
        {
        }

        public int Insert(User user, bool rollback = false)
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


        public int Update(User user, bool rollback = false)
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

        public int Delete(User user, bool rollback = false)
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

        public int Load(User user, bool rollback = false)
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

        public int LoadById(User user, bool rollback = false)
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
