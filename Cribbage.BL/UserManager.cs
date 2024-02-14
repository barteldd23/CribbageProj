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
                int results;
                using (CribbageEntities dc = new CribbageEntities()) 
                {
                    // Check if the email exists (can't have duplicates)
                    bool inUse = dc.tblUsers.Any(u => u.Email.Trim().ToUpper() == user.Email.Trim().ToUpper());

                    if (inUse && rollback == false) 
                    {
                        throw new Exception("The provided email is already associated with an account.");
                    }
                    else
                    {
                        IDbContextTransaction transaction = null;
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        tblUser newUser = new tblUser();

                        newUser.Id = Guid.NewGuid();
                        newUser.Email = user.Email;
                        newUser.DisplayName = user.DisplayName;
                        newUser.FirstName = user.FirstName;
                        newUser.LastName = user.LastName;
                        newUser.Password = user.Password;
                        newUser.GamesStarted = user.GamesStarted;
                        newUser.GamesWon = user.GamesWon;
                        newUser.GamesLost = user.GamesLost;
                        newUser.WinStreak = user.WinStreak;
                        newUser.AvgPtsPerGame = user.AvgPtsPerGame;
                        newUser.AvgHandScore = user.AvgHandScore;

                        user.Id = newUser.Id;

                        dc.tblUsers.Add(newUser);

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                }
                return results;
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
                int results;

                using (CribbageEntities dc =  new CribbageEntities())
                {
                    // Check if the email already exists - can't have duplicate emails listed
                    tblUser existingUser = dc.tblUsers.Where(u => u.Email.Trim().ToUpper() == user.Email.Trim().ToUpper()).FirstOrDefault();
                    
                    if (existingUser != null && existingUser.Id != user.Id && rollback == false)
                    {
                        throw new Exception("The provided email is already associated with an account.");
                    }
                    else
                    {
                        IDbContextTransaction transaction = null;
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        tblUser updateRow = dc.tblUsers.FirstOrDefault(r => r.Id == user.Id);

                        if (updateRow != null)
                        {
                            updateRow.Email = user.Email.Trim();
                            updateRow.Password = GetHash(user.Password.Trim());
                            updateRow.FirstName = user.FirstName.Trim();
                            updateRow.LastName = user.LastName.Trim();

                            dc.tblUsers.Update(updateRow);

                            results = dc.SaveChanges();

                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row not found.");
                        }
                    }
                }

                return 1;
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

                using (CribbageEntities dc = new CribbageEntities())
                {
                    // Check if the user is associated with a game - do not delete 
                    bool inUse = dc.tblUserGames.Any(u => u.Id == id);

                    if (inUse)
                    {
                        throw new Exception("This user is associated with a game, and cannot be deleted.");
                    }
                    else
                    {
                        IDbContextTransaction transaction = null;
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        tblUser deleteRow = dc.tblUsers.FirstOrDefault(r => r.Id == id);

                        if (deleteRow != null)
                        {
                            dc.tblUsers.Remove(deleteRow);

                            results= dc.SaveChanges();

                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row not found.");
                        }
                    }
                }

                return results;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public List<User> Load(User user, bool rollback = false)
        {
            try
            {
                List<User> users = new List<User>();

                base.Load()
                    .ForEach(u => users
                    .Add(new User
                    {
                        Id = u.Id,
                        Email = u.Email,
                        DisplayName = u.DisplayName,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Password = u.Password,
                        GamesStarted = u.GamesStarted,
                        GamesWon = u.GamesWon,
                        GamesLost = u.GamesLost,
                        WinStreak = u.WinStreak,
                        AvgPtsPerGame = u.AvgPtsPerGame,
                        AvgHandScore = u.AvgHandScore
                    }));
                return users;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public User LoadById(Guid id)
        {
            try
            {
                User user = new User();

                using (CribbageEntities dc = new CribbageEntities())
                {
                    user = (from u in dc.tblUsers
                            where u.Id == id
                            select new User
                            {
                                Id = u.Id,
                                Email = u.Email,
                                DisplayName = u.DisplayName,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                Password = u.Password,
                                GamesStarted = u.GamesStarted,
                                GamesWon = u.GamesWon,
                                GamesLost = u.GamesLost,
                                WinStreak = u.WinStreak,
                                AvgPtsPerGame = u.AvgPtsPerGame,
                                AvgHandScore = u.AvgHandScore
                            }).FirstOrDefault();
                }
                return user;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
