using Cribbage.BL.Models;
using System.Security.Cryptography;

namespace Cribbage.BL
{
    public class UserManager : GenericManager<tblUser>
    {
        public class LoginFailureException : Exception
        {
            public LoginFailureException() : base("Cannot log in with the provided credentials.")
            {
            }

            public LoginFailureException(string message) : base(message)
            {
            }
        }

        public UserManager(DbContextOptions<CribbageEntities> options) : base(options)
        {
        }

        private string GetHash(string Password)
        {
            using (var hasher = new SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(Password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }

        public void Seed()
        {
            List<User> users = new List<User>();

            foreach (User user in users)
            {
                if (user.Password.Length != 28)
                {
                    Update(user);
                }
            }

            if (users.Count == 0)
            {
                Insert(new User { Email = "test1@test.com", FirstName = "Test1", LastName = "Testing1", Password = "test" });
                Insert(new User { Email = "test2@test.com", FirstName = "Test2", LastName = "Testing2", Password = "test" });
                Insert(new User { Email = "computer@test.com", FirstName = "Computer", LastName = "Testing", Password = "test" });

            }
        }

        public bool Login(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Email))
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        using (CribbageEntities dc = new CribbageEntities(options))
                        {
                            tblUser userrow = dc.tblUsers.FirstOrDefault(u => u.Email == user.Email);

                            if (userrow != null)
                            {
                                // check the password 
                                if (userrow.Password == GetHash(user.Password))
                                {
                                    // Login was successful
                                    user.Id = userrow.Id;
                                    user.Email = userrow.Email;
                                    user.DisplayName = userrow.DisplayName;
                                    user.FirstName = userrow.FirstName;
                                    user.LastName = userrow.LastName;
                                    user.Password = userrow.Password;
                                    user.GamesStarted = userrow.GamesStarted;
                                    user.GamesWon = userrow.GamesWon;
                                    user.GamesLost = userrow.GamesLost;
                                    user.WinStreak = userrow.WinStreak;
                                    user.AvgPtsPerGame = userrow.AvgPtsPerGame;
                                    user.AvgHandScore = userrow.AvgHandScore;

                                    return true;
                                }
                                else
                                {
                                    throw new LoginFailureException("Cannot log in with the provided credentials.");
                                }
                            }
                            else
                            {
                                throw new Exception("User not found.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Password not set.");
                    }
                }
                else
                {
                    throw new Exception("Email not set.");
                }
            }
            catch (Exception e)
            {

                throw e;
            }
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
