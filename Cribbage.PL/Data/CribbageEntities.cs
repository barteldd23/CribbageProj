using Cribbage.PL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cribbage.PL.Data
{
    public class CribbageEntities : DbContext
    {
        Guid[] gameId = new Guid[5];
        Guid[] userId = new Guid[5];
        Guid[] userGameId = new Guid[10];

        public virtual DbSet<tblUser> tblUsers { get; set; }

        public virtual DbSet<tblGame> tblGames { get; set; }

        public virtual DbSet<tblUserGame> tblUserGames { get; set; }

        public CribbageEntities(DbContextOptions<CribbageEntities> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //    .EnableSensitiveDataLogging()
            //    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
        }

        public CribbageEntities()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            CreateUsers(modelBuilder);
            CreateGames(modelBuilder);
            CreateUserGames(modelBuilder);
        }

        private void CreateGames(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < gameId.Length; i++)
            {
                gameId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblGame>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblGame_Id");

                entity.ToTable("tblGame");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<tblGame>().HasData(new tblGame
            {
                Id = gameId[0],
                GameName = "Test1",
                Winner = userId[1],
                Date = new DateTime(2023, 10, 6),
                Complete = true
            });

            modelBuilder.Entity<tblGame>().HasData(new tblGame
            {
                Id = gameId[1],
                GameName = "Test2",
                Winner = userId[3],
                Date = new DateTime(2023, 11, 14),
                Complete = true
            });

            modelBuilder.Entity<tblGame>().HasData(new tblGame
            {
                Id = gameId[2],
                GameName = "Test3",
                Winner = userId[2],
                Date = new DateTime(2023, 12, 20),
                Complete = true
            });

            modelBuilder.Entity<tblGame>().HasData(new tblGame
            {
                Id = gameId[3],
                GameName = "Test4",
                Winner = userId[0],
                Date = new DateTime(2024, 1, 12),
                Complete = true
            });

            modelBuilder.Entity<tblGame>().HasData(new tblGame
            {
                Id = gameId[4],
                GameName = "Test5",
                Winner = userId[0],
                Date = new DateTime(2024, 2, 4),
                Complete = true
            });
        }

        private void CreateUsers(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < userId.Length; i++)
            {
                userId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblUser>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblUser_Id");

                entity.ToTable("tblUser");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[0],
                Email = "cribbage@game.com",
                DisplayName = "CardMaster",
                FirstName = "Joe",
                LastName = "Smith",
                Password = GetHash("maple"),
                GamesStarted = 1,
                GamesWon = 2,
                GamesLost = 0,
                WinStreak = 2,
                AvgPtsPerGame = 121,
                //AvgHandScore = 15
            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[1],
                Email = "fun@yahoo.com",
                DisplayName = "CribbageBox",
                FirstName = "Peter",
                LastName = "Parker",
                Password = GetHash("maple"),
                GamesStarted = 2,
                GamesWon = 1,
                GamesLost = 1,
                WinStreak = 1,
                AvgPtsPerGame = 103,
                //AvgHandScore = 10
            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[2],
                Email = "cards@me.com",
                DisplayName = "GamesRCool",
                FirstName = "Kelly",
                LastName = "Bot",
                Password = GetHash("maple"),
                GamesStarted = 4,
                GamesWon = 1,
                GamesLost = 3,
                WinStreak = 1,
                AvgPtsPerGame = 82.75,
                //AvgHandScore = 8
            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[3],
                Email = "tester@gmail.com",
                DisplayName = "Testing",
                FirstName = "Test",
                LastName = "Tester",
                Password = GetHash("maple"),
                GamesStarted = 1,
                GamesWon = 1,
                GamesLost = 0,
                WinStreak = 1,
                AvgPtsPerGame = 121,
                //AvgHandScore = 20
            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[4],
                Email = "computer@computer.com",
                DisplayName = "Computer",
                FirstName = "Computer",
                LastName = "Computer",
                Password = GetHash("maple"),
                GamesStarted = 1,
                GamesWon = 0,
                GamesLost = 0,
                WinStreak = 0,
                AvgPtsPerGame = 50,
                //AvgHandScore = 5
            });
        }

        private void CreateUserGames(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < userGameId.Length; i++)
            {
                userGameId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblUserGame>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblUserGame_Id");

                entity.ToTable("tblUserGame");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                 .WithMany(p => p.tblUserGames)
                 .HasForeignKey(d => d.PlayerId)
                 .HasConstraintName("fk_tblUserGame_UserId");

                entity.HasOne(d => d.Game)
                .WithMany(p => p.tblUserGames)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("fk_tblUserGame_GameId");
            });

            List<tblUserGame> UserGames = new List<tblUserGame>
            {
                new tblUserGame {Id = userGameId[0], GameId = gameId[0], PlayerId = userId[1], PlayerScore = 121},
                new tblUserGame {Id = userGameId[1], GameId = gameId[0], PlayerId = userId[2], PlayerScore = 70},
                new tblUserGame {Id = userGameId[2], GameId = gameId[1], PlayerId = userId[2], PlayerScore = 90},
                new tblUserGame {Id = userGameId[3], GameId = gameId[1], PlayerId = userId[3], PlayerScore = 121},
                new tblUserGame {Id = userGameId[4], GameId = gameId[2], PlayerId = userId[2], PlayerScore = 121},
                new tblUserGame {Id = userGameId[5], GameId = gameId[2], PlayerId = userId[1], PlayerScore = 85},
                new tblUserGame {Id = userGameId[6], GameId = gameId[3], PlayerId = userId[0], PlayerScore = 121},
                new tblUserGame {Id = userGameId[7], GameId = gameId[3], PlayerId = userId[2], PlayerScore = 50},
                new tblUserGame {Id = userGameId[8], GameId = gameId[4], PlayerId = userId[0], PlayerScore = 121},
                new tblUserGame {Id = userGameId[9], GameId = gameId[4], PlayerId = userId[4], PlayerScore = 50}
            };

            modelBuilder.Entity<tblUserGame>().HasData(UserGames);
        }

        private static string GetHash(string Password)
        {
            using (var hasher = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(Password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }
    }
}
