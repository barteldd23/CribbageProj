﻿// <auto-generated />
using System;
using Cribbage.PL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cribbage.PL.Migrations
{
    [DbContext(typeof(CribbageEntities))]
    [Migration("20240202162530_sp-GetUsers")]
    partial class spGetUsers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cribbage.PL.Entities.tblGame", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<Guid>("Player_1_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Player_1_Score")
                        .HasColumnType("int");

                    b.Property<Guid>("Player_2_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Player_2_Score")
                        .HasColumnType("int");

                    b.Property<Guid>("Winner")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK_tblGame_Id");

                    b.ToTable("tblGame", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("4f513ed2-a3b1-4161-9eab-66b16fbde479"),
                            Date = new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Player_1_Id = new Guid("20425efd-8ece-47c9-90b7-e2e9a0df1686"),
                            Player_1_Score = 121,
                            Player_2_Id = new Guid("329ae371-22b0-420e-be50-248157a2b083"),
                            Player_2_Score = 70,
                            Winner = new Guid("20425efd-8ece-47c9-90b7-e2e9a0df1686")
                        },
                        new
                        {
                            Id = new Guid("bf356195-3c43-4285-b54e-22587b1ea7e2"),
                            Date = new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Player_1_Id = new Guid("329ae371-22b0-420e-be50-248157a2b083"),
                            Player_1_Score = 90,
                            Player_2_Id = new Guid("022920f8-16d2-4050-8d65-c104b8d3e637"),
                            Player_2_Score = 121,
                            Winner = new Guid("022920f8-16d2-4050-8d65-c104b8d3e637")
                        },
                        new
                        {
                            Id = new Guid("4bd2973f-07a5-42d3-ac81-99411ef2facd"),
                            Date = new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Player_1_Id = new Guid("329ae371-22b0-420e-be50-248157a2b083"),
                            Player_1_Score = 121,
                            Player_2_Id = new Guid("20425efd-8ece-47c9-90b7-e2e9a0df1686"),
                            Player_2_Score = 85,
                            Winner = new Guid("329ae371-22b0-420e-be50-248157a2b083")
                        },
                        new
                        {
                            Id = new Guid("239f90bb-f639-43ca-b0e6-eb26218656df"),
                            Date = new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Player_1_Id = new Guid("8767e911-ae32-48ab-a0ce-9e3c2ad07c1d"),
                            Player_1_Score = 121,
                            Player_2_Id = new Guid("329ae371-22b0-420e-be50-248157a2b083"),
                            Player_2_Score = 50,
                            Winner = new Guid("8767e911-ae32-48ab-a0ce-9e3c2ad07c1d")
                        });
                });

            modelBuilder.Entity("Cribbage.PL.Entities.tblUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("AvgPtsPerGame")
                        .HasColumnType("float");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GamesStarted")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WinStreak")
                        .HasColumnType("int");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_tblUser_Id");

                    b.ToTable("tblUser", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("8767e911-ae32-48ab-a0ce-9e3c2ad07c1d"),
                            AvgPtsPerGame = 121.0,
                            DisplayName = "CardMaster",
                            Email = "cribbage@game.com",
                            FirstName = "Joe",
                            GamesStarted = 1,
                            LastName = "Smith",
                            Losses = 0,
                            Password = "pYfdnNb8sO0FgS4H0MRSwLGOIME=",
                            WinStreak = 1,
                            Wins = 1
                        },
                        new
                        {
                            Id = new Guid("20425efd-8ece-47c9-90b7-e2e9a0df1686"),
                            AvgPtsPerGame = 103.0,
                            DisplayName = "CribbageBox",
                            Email = "fun@yahoo.com",
                            FirstName = "Peter",
                            GamesStarted = 2,
                            LastName = "Parker",
                            Losses = 1,
                            Password = "pYfdnNb8sO0FgS4H0MRSwLGOIME=",
                            WinStreak = 1,
                            Wins = 1
                        },
                        new
                        {
                            Id = new Guid("329ae371-22b0-420e-be50-248157a2b083"),
                            AvgPtsPerGame = 82.75,
                            DisplayName = "GamesRCool",
                            Email = "cards@me.com",
                            FirstName = "Kelly",
                            GamesStarted = 4,
                            LastName = "Bot",
                            Losses = 3,
                            Password = "pYfdnNb8sO0FgS4H0MRSwLGOIME=",
                            WinStreak = 1,
                            Wins = 1
                        },
                        new
                        {
                            Id = new Guid("022920f8-16d2-4050-8d65-c104b8d3e637"),
                            AvgPtsPerGame = 121.0,
                            DisplayName = "Testing",
                            Email = "tester@gmail.com",
                            FirstName = "Test",
                            GamesStarted = 1,
                            LastName = "Tester",
                            Losses = 0,
                            Password = "pYfdnNb8sO0FgS4H0MRSwLGOIME=",
                            WinStreak = 1,
                            Wins = 1
                        });
                });

            modelBuilder.Entity("Cribbage.PL.Entities.tblUserGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK_tblUserGame_Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("tblUserGame", (string)null);

                    b.HasData(
                        new
                        {
                            Id = -99,
                            GameId = new Guid("4f513ed2-a3b1-4161-9eab-66b16fbde479"),
                            UserId = new Guid("20425efd-8ece-47c9-90b7-e2e9a0df1686")
                        },
                        new
                        {
                            Id = -98,
                            GameId = new Guid("bf356195-3c43-4285-b54e-22587b1ea7e2"),
                            UserId = new Guid("022920f8-16d2-4050-8d65-c104b8d3e637")
                        },
                        new
                        {
                            Id = -97,
                            GameId = new Guid("4bd2973f-07a5-42d3-ac81-99411ef2facd"),
                            UserId = new Guid("329ae371-22b0-420e-be50-248157a2b083")
                        },
                        new
                        {
                            Id = -96,
                            GameId = new Guid("239f90bb-f639-43ca-b0e6-eb26218656df"),
                            UserId = new Guid("8767e911-ae32-48ab-a0ce-9e3c2ad07c1d")
                        });
                });

            modelBuilder.Entity("Cribbage.PL.Entities.tblUserGame", b =>
                {
                    b.HasOne("Cribbage.PL.Entities.tblGame", "Game")
                        .WithMany("tblUserGames")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tblUserGame_GameId");

                    b.HasOne("Cribbage.PL.Entities.tblUser", "User")
                        .WithMany("tblUserGames")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tblUserGame_UserId");

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cribbage.PL.Entities.tblGame", b =>
                {
                    b.Navigation("tblUserGames");
                });

            modelBuilder.Entity("Cribbage.PL.Entities.tblUser", b =>
                {
                    b.Navigation("tblUserGames");
                });
#pragma warning restore 612, 618
        }
    }
}
