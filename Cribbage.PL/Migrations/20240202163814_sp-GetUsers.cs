using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cribbage.PL.Migrations
{
    /// <inheritdoc />
    public partial class spGetUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetUsers]
                    @FirstName varchar(50)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    select * from Users where FirstName like @FirstName +'%'
                END";

            migrationBuilder.Sql(sp);

            migrationBuilder.CreateTable(
                name: "tblGame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player_1_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player_2_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player_1_Score = table.Column<int>(type: "int", nullable: false),
                    Player_2_Score = table.Column<int>(type: "int", nullable: false),
                    Winner = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        column: x => x.UserId,
                        principalTable: "tblUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblGame",
                columns: new[] { "Id", "Date", "Player_1_Id", "Player_1_Score", "Player_2_Id", "Player_2_Score", "Winner" },
                values: new object[,]
                {
                    { new Guid("20a8c4ec-29c9-4dac-9011-24119ef547ad"), new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("31f52f45-8406-4e10-99f7-a54727b88ad0"), 121, new Guid("ef596e64-a01b-469f-917a-9b0c0c95a9e3"), 70, new Guid("31f52f45-8406-4e10-99f7-a54727b88ad0") },
                    { new Guid("4f7ad27d-17b1-4088-a0c4-54830ffa2370"), new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef596e64-a01b-469f-917a-9b0c0c95a9e3"), 121, new Guid("31f52f45-8406-4e10-99f7-a54727b88ad0"), 85, new Guid("ef596e64-a01b-469f-917a-9b0c0c95a9e3") },
                    { new Guid("b84842c4-9da4-498c-98e3-db4faedfd655"), new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1b27ad36-0bce-433f-9d4c-a1962f24616d"), 121, new Guid("ef596e64-a01b-469f-917a-9b0c0c95a9e3"), 50, new Guid("1b27ad36-0bce-433f-9d4c-a1962f24616d") },
                    { new Guid("fff93340-5460-4ab7-82ab-91c58bcaf03a"), new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef596e64-a01b-469f-917a-9b0c0c95a9e3"), 90, new Guid("36e2e132-2633-451d-8b0f-a740ae2cd663"), 121, new Guid("36e2e132-2633-451d-8b0f-a740ae2cd663") }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "AvgPtsPerGame", "DisplayName", "Email", "FirstName", "GamesStarted", "LastName", "Losses", "Password", "WinStreak", "Wins" },
                values: new object[,]
                {
                    { new Guid("1b27ad36-0bce-433f-9d4c-a1962f24616d"), 121.0, "CardMaster", "cribbage@game.com", "Joe", 1, "Smith", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("31f52f45-8406-4e10-99f7-a54727b88ad0"), 103.0, "CribbageBox", "fun@yahoo.com", "Peter", 2, "Parker", 1, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("36e2e132-2633-451d-8b0f-a740ae2cd663"), 121.0, "Testing", "tester@gmail.com", "Test", 1, "Tester", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("ef596e64-a01b-469f-917a-9b0c0c95a9e3"), 82.75, "GamesRCool", "cards@me.com", "Kelly", 4, "Bot", 3, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "UserId" },
                values: new object[,]
                {
                    { -99, new Guid("20a8c4ec-29c9-4dac-9011-24119ef547ad"), new Guid("31f52f45-8406-4e10-99f7-a54727b88ad0") },
                    { -98, new Guid("fff93340-5460-4ab7-82ab-91c58bcaf03a"), new Guid("36e2e132-2633-451d-8b0f-a740ae2cd663") },
                    { -97, new Guid("4f7ad27d-17b1-4088-a0c4-54830ffa2370"), new Guid("ef596e64-a01b-469f-917a-9b0c0c95a9e3") },
                    { -96, new Guid("b84842c4-9da4-498c-98e3-db4faedfd655"), new Guid("1b27ad36-0bce-433f-9d4c-a1962f24616d") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblUserGame_GameId",
                table: "tblUserGame",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserGame_UserId",
                table: "tblUserGame",
                column: "UserId");
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
