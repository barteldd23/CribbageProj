﻿using Cribbage.BL.Models;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;
using System.Security.Cryptography;

namespace Cribbage.BL
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

    public class UserManager : GenericManager<tblUser>
    {
        public UserManager(DbContextOptions<CribbageEntities> options) : base(options) { }

        public UserManager(ILogger logger, DbContextOptions<CribbageEntities> options) : base(logger, options) { }

        private string GetHash(string Password)
        {
            using (var hasher = new SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(Password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
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
                                    //user.AvgHandScore = userrow.AvgHandScore;

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

        public async Task<int> InsertAsync(User user, bool rollback = false)
        {
            try
            {
                tblUser row = new tblUser { Email = user.Email };
                user.Id = row.Id;
                return await InsertAsync(row, e => e.Email == user.Email, rollback);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<User>> LoadAsync()
        {
            try
            {
                List<User> rows = new List<User>();
                (await base.LoadAsync())
                    .OrderBy(d => d.SortField)
                    .ToList()
                    .ForEach(d => rows.Add(
                        new User
                        {
                            Id = d.Id,
                            Email = d.Email,
                            Password = d.Password,
                            DisplayName = d.DisplayName,
                            FirstName = d.FirstName,
                            LastName = d.LastName,
                            GamesStarted = d.GamesStarted,
                            GamesWon = d.GamesWon,
                            GamesLost = d.GamesLost,
                            WinStreak = d.WinStreak,
                            AvgPtsPerGame = d.AvgPtsPerGame
            }));
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(User user, bool rollback = false)
        {
            try
            {
                int results;
                using (CribbageEntities dc = new CribbageEntities(options)) 
                {
                    // Check if the email exists (can't have duplicates)
                    bool inUse = dc.tblUsers.Any(u => u.Email.Trim().ToUpper() == user.Email.Trim().ToUpper());

                    if (inUse) 
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
                        newUser.Password = GetHash(user.Password.Trim());
                        newUser.GamesStarted = 0;
                        newUser.GamesWon = 0;
                        newUser.GamesLost = 0;
                        newUser.WinStreak = 0;
                        newUser.AvgPtsPerGame = 0;
                        //newUser.AvgHandScore = 0;

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

                using (CribbageEntities dc =  new CribbageEntities(options))
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
                            //updateRow.Password = GetHash(user.Password.Trim());
                            updateRow.FirstName = user.FirstName.Trim();
                            updateRow.LastName = user.LastName.Trim();
                            updateRow.GamesStarted = user.GamesStarted;
                            updateRow.GamesWon = user.GamesWon;
                            updateRow.GamesLost = user.GamesLost;
                            updateRow.WinStreak = user.WinStreak;
                            updateRow.AvgPtsPerGame = user.AvgPtsPerGame;
                            //updateRow.AvgHandScore = user.AvgHandScore;

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

                using (CribbageEntities dc = new CribbageEntities(options))
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

        public List<User> Load()
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
                        //AvgHandScore = u.AvgHandScore
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
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    tblUser row = dc.tblUsers.FirstOrDefault(u => u.Id == id);

                    if (row != null)
                    {
                        User user = new User
                        {
                            Id = row.Id,
                            Email = row.Email,
                            DisplayName = row.DisplayName,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            Password = row.Password,
                            GamesStarted = row.GamesStarted,
                            GamesWon = row.GamesWon,
                            GamesLost = row.GamesLost,
                            WinStreak = row.WinStreak,
                            AvgPtsPerGame = row.AvgPtsPerGame,
                            //AvgHandScore = row.AvgHandScore
                        };
                        return user;
                    }
                    else
                    {
                        throw new Exception("Row not found.");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public User LoadByEmail(String email)
        {
            try
            {
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    tblUser row = dc.tblUsers.FirstOrDefault(u => u.Email == email);

                    if (row != null)
                    {
                        User user = new User
                        {
                            Id = row.Id,
                            Email = row.Email,
                            DisplayName = row.DisplayName,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            Password = row.Password,
                            GamesStarted = row.GamesStarted,
                            GamesWon = row.GamesWon,
                            GamesLost = row.GamesLost,
                            WinStreak = row.WinStreak,
                            AvgPtsPerGame = row.AvgPtsPerGame,
                            //AvgHandScore = row.AvgHandScore
                        };
                        return user;
                    }
                    else
                    {
                        throw new Exception("Row not found.");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<spGetMostWinsResult> LoadSPGetMostWins()
        {
            try
            {
                List<spGetMostWinsResult> users = new List<spGetMostWinsResult>();

                base.Load("spGetMostWins")
                    .ForEach(u => users.Add(
                        new spGetMostWinsResult
                        {
                            DisplayName = u.DisplayName,
                            GamesWon = u.GamesWon
                        }));
                return users;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
