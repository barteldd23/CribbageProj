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
                    GameName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GamesStarted = table.Column<int>(type: "int", nullable: false),
                    GamesWon = table.Column<int>(type: "int", nullable: false),
                    GamesLost = table.Column<int>(type: "int", nullable: false),
                    WinStreak = table.Column<int>(type: "int", nullable: false),
                    AvgPtsPerGame = table.Column<double>(type: "float", nullable: false),
                    AvgHandScore = table.Column<double>(type: "float", nullable: false)
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
                columns: new[] { "Id", "Complete", "Date", "GameName", "Winner" },
                values: new object[,]
                {
                    { new Guid("3ce83c42-2d6c-43d3-93d3-c29e0f455067"), true, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test4", new Guid("361b8dab-0966-4f5b-af46-56dd34de727b") },
                    { new Guid("4e639a75-9603-491f-8fe2-63922c146c30"), true, new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test3", new Guid("d5c0c33f-8456-4113-9044-54b2cb904f37") },
                    { new Guid("8592b201-79f5-4c8e-9b6c-c4a8fe577e76"), true, new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test2", new Guid("8622d459-75d3-425b-8d15-e7834ca26be9") },
                    { new Guid("90a91d1c-b521-4e7c-a0c5-ed1f60204541"), true, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test5", new Guid("361b8dab-0966-4f5b-af46-56dd34de727b") },
                    { new Guid("f900b97b-6bfa-48ba-9ba2-0a11a3baf555"), true, new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test1", new Guid("3285898c-cb80-4292-8ebd-e7a872d7ad78") }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "AvgHandScore", "AvgPtsPerGame", "DisplayName", "Email", "FirstName", "GamesLost", "GamesStarted", "GamesWon", "LastName", "Password", "WinStreak" },
                values: new object[,]
                {
                    { new Guid("3285898c-cb80-4292-8ebd-e7a872d7ad78"), 10.0, 103.0, "CribbageBox", "fun@yahoo.com", "Peter", 1, 2, 1, "Parker", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1 },
                    { new Guid("361b8dab-0966-4f5b-af46-56dd34de727b"), 15.0, 121.0, "CardMaster", "cribbage@game.com", "Joe", 0, 1, 2, "Smith", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 2 },
                    { new Guid("7be3bf63-9002-4311-8406-0a9ffbb86b63"), 5.0, 50.0, "Computer", "computer@computer.com", "Computer", 0, 1, 0, "Computer", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 0 },
                    { new Guid("8622d459-75d3-425b-8d15-e7834ca26be9"), 20.0, 121.0, "Testing", "tester@gmail.com", "Test", 0, 1, 1, "Tester", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1 },
                    { new Guid("d5c0c33f-8456-4113-9044-54b2cb904f37"), 8.0, 82.75, "GamesRCool", "cards@me.com", "Kelly", 3, 4, 1, "Bot", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "PlayerId", "PlayerScore" },
                values: new object[,]
                {
                    { new Guid("056b3725-1c04-41e4-b29b-fd659dd55586"), new Guid("3ce83c42-2d6c-43d3-93d3-c29e0f455067"), new Guid("d5c0c33f-8456-4113-9044-54b2cb904f37"), 50 },
                    { new Guid("34f5dead-980f-4fd1-b541-120ce80da46c"), new Guid("90a91d1c-b521-4e7c-a0c5-ed1f60204541"), new Guid("7be3bf63-9002-4311-8406-0a9ffbb86b63"), 50 },
                    { new Guid("482f351f-6e01-4506-babc-0f47e380deac"), new Guid("f900b97b-6bfa-48ba-9ba2-0a11a3baf555"), new Guid("d5c0c33f-8456-4113-9044-54b2cb904f37"), 70 },
                    { new Guid("5ad6de29-2ed3-472f-95f1-f468fa513f29"), new Guid("90a91d1c-b521-4e7c-a0c5-ed1f60204541"), new Guid("361b8dab-0966-4f5b-af46-56dd34de727b"), 121 },
                    { new Guid("68555fa8-5342-4d0e-a40d-dacb7c2b28d0"), new Guid("4e639a75-9603-491f-8fe2-63922c146c30"), new Guid("d5c0c33f-8456-4113-9044-54b2cb904f37"), 121 },
                    { new Guid("71e2ae3e-70d7-49a3-a05d-d06b50f1b92b"), new Guid("f900b97b-6bfa-48ba-9ba2-0a11a3baf555"), new Guid("3285898c-cb80-4292-8ebd-e7a872d7ad78"), 121 },
                    { new Guid("9eab3f87-13fd-4df9-9128-f16007b99f19"), new Guid("8592b201-79f5-4c8e-9b6c-c4a8fe577e76"), new Guid("d5c0c33f-8456-4113-9044-54b2cb904f37"), 90 },
                    { new Guid("b8c5287d-bdca-4543-8cb4-bb529f3eef85"), new Guid("4e639a75-9603-491f-8fe2-63922c146c30"), new Guid("3285898c-cb80-4292-8ebd-e7a872d7ad78"), 85 },
                    { new Guid("cdc3a664-9222-481d-b927-f03f129979a7"), new Guid("8592b201-79f5-4c8e-9b6c-c4a8fe577e76"), new Guid("8622d459-75d3-425b-8d15-e7834ca26be9"), 121 },
                    { new Guid("fc872e76-5ff0-43c1-83db-a71fb9230bbc"), new Guid("3ce83c42-2d6c-43d3-93d3-c29e0f455067"), new Guid("361b8dab-0966-4f5b-af46-56dd34de727b"), 121 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_Email",
                table: "tblUser",
                column: "Email",
                unique: true);

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
