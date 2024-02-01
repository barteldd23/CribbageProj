using Cribbage.PL.Entities;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage.PL.Data
{
    public class CribbageEntities : DbContext
    {
        // Do you think these are just setting up arrays for default data? -Dean
        // Yes, I'll start setting this up - Rachel

        Guid[] gameId = new Guid[4];
        Guid[] player_1_Id = new Guid[4];
        Guid[] player_2_Id = new Guid[4];
        Guid[] userId = new Guid[4];

        public virtual DbSet<tblUser> tblUsers { get; set; }

        public virtual DbSet<tblGame> tblGames { get; set; }

        public virtual DbSet<tblUserGame> tblUserGames { get; set; }

        public CribbageEntities(DbContextOptions<CribbageEntities> options) : base(options)
        {
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

        private void CreateUserGames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblUserGame>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblUserGames_Id");

                entity.ToTable("tblUserGames");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                 .WithMany(p => p.tblUserGames)
                 .HasForeignKey(d => d.UserId)
                 .HasConstraintName("tblUserGame_UserId");

                entity.HasOne(d => d.Game)
                .WithMany(p => p.tblUserGames)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("tblUserGame_GameId");
            });

            List<tblUserGame> UserGames = new List<tblUserGame>
            {
                new tblUserGame {Id = Guid.NewGuid(), GameId = gameId[2], UserId = userId[0]},
                new tblUserGame {Id = Guid.NewGuid(), GameId = gameId[3], UserId = userId[0]},
                new tblUserGame {Id = Guid.NewGuid(), GameId = gameId[4], UserId = userId[0]},
                new tblUserGame {Id = Guid.NewGuid(), GameId = gameId[1], UserId = userId[1]},
            };

            modelBuilder.Entity<tblUserGame>().HasData(UserGames);
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
                Player_1_Id = player_1_Id[1],
                Player_2_Id = player_2_Id[2],
                Player_1_Score = 121,
                Player_2_Score = 70,
                Winner = player_1_Id[1],
                Date = new DateTime(2023, 10, 6)
            });

            modelBuilder.Entity<tblGame>().HasData(new tblGame
            {
                Id = gameId[0],
                Player_1_Id = player_1_Id[2],
                Player_2_Id = player_2_Id[3],
                Player_1_Score = 90,
                Player_2_Score = 121,
                Winner = player_2_Id[3],
                Date = new DateTime(2023, 11, 14)
            });

            modelBuilder.Entity<tblGame>().HasData(new tblGame
            {
                Id = gameId[0],
                Player_1_Id = player_1_Id[2],
                Player_2_Id = player_2_Id[1],
                Player_1_Score = 121,
                Player_2_Score = 85,
                Winner = player_1_Id[2],
                Date = new DateTime(2023, 12, 20)
            });

            modelBuilder.Entity<tblGame>().HasData(new tblGame
            {
                Id = gameId[0],
                Player_1_Id = player_1_Id[0],
                Player_2_Id = player_2_Id[2],
                Player_1_Score = 121,
                Player_2_Score = 50,
                Winner = player_1_Id[0],
                Date = new DateTime(2024, 1, 12)
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
                Wins = 1,
                Losses = 0,
                AvgPtsPerGame = 121,
                WinStreak = 1
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
                Wins = 1,
                Losses = 1,
                AvgPtsPerGame = 103,
                WinStreak = 1
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
                Wins = 1,
                Losses = 3,
                AvgPtsPerGame = 82.75,
                WinStreak = 1
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
                Wins = 1,
                Losses = 0,
                AvgPtsPerGame = 121,
                WinStreak = 1
            });
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
