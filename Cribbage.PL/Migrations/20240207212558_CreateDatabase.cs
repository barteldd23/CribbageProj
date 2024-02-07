using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cribbage.PL.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Winner = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Complete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGame_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GamesStarted = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false),
                    AvgPtsPerGame = table.Column<double>(type: "float", nullable: false),
                    WinStreak = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUserGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserGame_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_tblUserGame_GameId",
                        column: x => x.GameId,
                        principalTable: "tblGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tblUserGame_UserId",
                        column: x => x.PlayerId,
                        principalTable: "tblUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblGame",
                columns: new[] { "Id", "Complete", "Date", "Winner" },
                values: new object[,]
                {
                    { new Guid("1eeb4e48-0052-4646-884b-5e382bd1b115"), true, new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2b86b984-5fe1-48ba-ad43-475cc807b4f2") },
                    { new Guid("86dae85b-4d1c-4241-94d8-c259b9cabb7a"), true, new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("482b11ef-e263-42b5-9e0b-4b97d59ee875") },
                    { new Guid("af5758c4-96b5-4df4-a2cc-28fef6ff0acb"), true, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("342bd2a2-4817-421e-999e-06996e0cd9a4") },
                    { new Guid("c639847c-1001-4017-8e26-50e7ca80a902"), true, new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5310802f-07a5-41d3-a247-00291bd28ef2") },
                    { new Guid("ede17980-2c16-4907-afc8-2f0c332743eb"), true, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("342bd2a2-4817-421e-999e-06996e0cd9a4") }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "AvgPtsPerGame", "DisplayName", "Email", "FirstName", "GamesStarted", "LastName", "Losses", "Password", "WinStreak", "Wins" },
                values: new object[,]
                {
                    { new Guid("1f441910-1161-445a-a2ec-a77fdde7e5ca"), 50.0, "Computer", "computer@computer.com", "Computer", 1, "Computer", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 0, 0 },
                    { new Guid("2b86b984-5fe1-48ba-ad43-475cc807b4f2"), 121.0, "Testing", "tester@gmail.com", "Test", 1, "Tester", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("342bd2a2-4817-421e-999e-06996e0cd9a4"), 121.0, "CardMaster", "cribbage@game.com", "Joe", 1, "Smith", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 2, 2 },
                    { new Guid("482b11ef-e263-42b5-9e0b-4b97d59ee875"), 103.0, "CribbageBox", "fun@yahoo.com", "Peter", 2, "Parker", 1, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("5310802f-07a5-41d3-a247-00291bd28ef2"), 82.75, "GamesRCool", "cards@me.com", "Kelly", 4, "Bot", 3, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "PlayerId", "PlayerScore" },
                values: new object[,]
                {
                    { new Guid("23008496-9d0a-4f8c-af05-e4b3b4ec6ed8"), new Guid("c639847c-1001-4017-8e26-50e7ca80a902"), new Guid("5310802f-07a5-41d3-a247-00291bd28ef2"), 121 },
                    { new Guid("4448fb17-da3c-4c23-a502-aad1fb47e69e"), new Guid("af5758c4-96b5-4df4-a2cc-28fef6ff0acb"), new Guid("1f441910-1161-445a-a2ec-a77fdde7e5ca"), 50 },
                    { new Guid("4e5f95da-60c5-4e7d-8cf4-8b7e2843314d"), new Guid("af5758c4-96b5-4df4-a2cc-28fef6ff0acb"), new Guid("342bd2a2-4817-421e-999e-06996e0cd9a4"), 121 },
                    { new Guid("6a821b15-685b-493a-8687-479008655bdd"), new Guid("86dae85b-4d1c-4241-94d8-c259b9cabb7a"), new Guid("482b11ef-e263-42b5-9e0b-4b97d59ee875"), 121 },
                    { new Guid("6ce5667e-3932-47b6-b641-dd3730c0915f"), new Guid("86dae85b-4d1c-4241-94d8-c259b9cabb7a"), new Guid("5310802f-07a5-41d3-a247-00291bd28ef2"), 70 },
                    { new Guid("8905bb7a-c0b1-4f18-8dad-c8caf2651a63"), new Guid("1eeb4e48-0052-4646-884b-5e382bd1b115"), new Guid("2b86b984-5fe1-48ba-ad43-475cc807b4f2"), 121 },
                    { new Guid("ade9eb5c-b878-43b8-a78a-458a7a3a9758"), new Guid("1eeb4e48-0052-4646-884b-5e382bd1b115"), new Guid("5310802f-07a5-41d3-a247-00291bd28ef2"), 90 },
                    { new Guid("aee3a793-bd31-4a55-b4aa-f5ab1f282cf4"), new Guid("c639847c-1001-4017-8e26-50e7ca80a902"), new Guid("482b11ef-e263-42b5-9e0b-4b97d59ee875"), 85 },
                    { new Guid("bf706849-c887-4b4a-93bc-c5dad6b2a5ab"), new Guid("ede17980-2c16-4907-afc8-2f0c332743eb"), new Guid("5310802f-07a5-41d3-a247-00291bd28ef2"), 50 },
                    { new Guid("d5c1b02a-d648-4cac-8997-24e69beebd78"), new Guid("ede17980-2c16-4907-afc8-2f0c332743eb"), new Guid("342bd2a2-4817-421e-999e-06996e0cd9a4"), 121 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblUserGame_GameId",
                table: "tblUserGame",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserGame_PlayerId",
                table: "tblUserGame",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblUserGame");

            migrationBuilder.DropTable(
                name: "tblGame");

            migrationBuilder.DropTable(
                name: "tblUser");
        }
    }
}
