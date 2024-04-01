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
                    AvgPtsPerGame = table.Column<double>(type: "float", nullable: false)
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
                    { new Guid("01bd0ed8-1392-4f47-9943-657977fdb42b"), true, new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test2", new Guid("e3ba195c-68e8-44ae-8c35-1175c3803ba6") },
                    { new Guid("20112213-9177-4a42-9543-15a22b66ba26"), true, new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test3", new Guid("a988fc08-9f27-46d0-a4fa-89769251fde3") },
                    { new Guid("bf691a8d-b655-4657-8c01-b15261cd3292"), true, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test5", new Guid("50bbeb2b-f32c-4b01-806f-b55ed37991c6") },
                    { new Guid("c320ea7d-5d2c-4038-a053-81a689f404c6"), true, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test4", new Guid("50bbeb2b-f32c-4b01-806f-b55ed37991c6") },
                    { new Guid("c4408f96-2976-4fe2-8492-c0c252ef2b3d"), true, new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test1", new Guid("7533a83e-d329-4af1-8abe-35c0793d9e9b") }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "AvgPtsPerGame", "DisplayName", "Email", "FirstName", "GamesLost", "GamesStarted", "GamesWon", "LastName", "Password", "WinStreak" },
                values: new object[,]
                {
                    { new Guid("150dc3dc-ae81-488c-86fc-941190a334cc"), 50.0, "Computer", "computer@computer.com", "Computer", 0, 1, 0, "Computer", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 0 },
                    { new Guid("50bbeb2b-f32c-4b01-806f-b55ed37991c6"), 121.0, "CardMaster", "cribbage@game.com", "Joe", 0, 1, 2, "Smith", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 2 },
                    { new Guid("7533a83e-d329-4af1-8abe-35c0793d9e9b"), 103.0, "CribbageBox", "fun@yahoo.com", "Peter", 1, 2, 1, "Parker", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1 },
                    { new Guid("a988fc08-9f27-46d0-a4fa-89769251fde3"), 82.75, "GamesRCool", "cards@me.com", "Kelly", 3, 4, 1, "Bot", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1 },
                    { new Guid("e3ba195c-68e8-44ae-8c35-1175c3803ba6"), 121.0, "Testing", "tester@gmail.com", "Test", 0, 1, 1, "Tester", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "PlayerId", "PlayerScore" },
                values: new object[,]
                {
                    { new Guid("0280b01f-a14d-4841-abdb-312e42f0999b"), new Guid("01bd0ed8-1392-4f47-9943-657977fdb42b"), new Guid("a988fc08-9f27-46d0-a4fa-89769251fde3"), 90 },
                    { new Guid("0dc0e147-9ca8-47af-8cfe-19a59e9b05b1"), new Guid("c4408f96-2976-4fe2-8492-c0c252ef2b3d"), new Guid("7533a83e-d329-4af1-8abe-35c0793d9e9b"), 121 },
                    { new Guid("3bdb25f8-7b06-4ac0-953c-4d7f6879ce76"), new Guid("c320ea7d-5d2c-4038-a053-81a689f404c6"), new Guid("a988fc08-9f27-46d0-a4fa-89769251fde3"), 50 },
                    { new Guid("554f727e-0a21-4236-9092-014dc1bd5ebb"), new Guid("20112213-9177-4a42-9543-15a22b66ba26"), new Guid("7533a83e-d329-4af1-8abe-35c0793d9e9b"), 85 },
                    { new Guid("83d9dab7-961e-4db7-9b3b-424b0804a3dd"), new Guid("c4408f96-2976-4fe2-8492-c0c252ef2b3d"), new Guid("a988fc08-9f27-46d0-a4fa-89769251fde3"), 70 },
                    { new Guid("915d1b6f-8c8f-4e9d-874d-508c7a23dfb4"), new Guid("01bd0ed8-1392-4f47-9943-657977fdb42b"), new Guid("e3ba195c-68e8-44ae-8c35-1175c3803ba6"), 121 },
                    { new Guid("a327a126-4fb0-4326-a70c-e527082cf2c3"), new Guid("20112213-9177-4a42-9543-15a22b66ba26"), new Guid("a988fc08-9f27-46d0-a4fa-89769251fde3"), 121 },
                    { new Guid("b20d5802-93c1-4560-946e-85678b076f01"), new Guid("bf691a8d-b655-4657-8c01-b15261cd3292"), new Guid("50bbeb2b-f32c-4b01-806f-b55ed37991c6"), 121 },
                    { new Guid("bd65b434-959e-4b5a-bfbd-61bb14011a68"), new Guid("bf691a8d-b655-4657-8c01-b15261cd3292"), new Guid("150dc3dc-ae81-488c-86fc-941190a334cc"), 50 },
                    { new Guid("dbe02981-3194-4643-a0c8-2bbff58f995d"), new Guid("c320ea7d-5d2c-4038-a053-81a689f404c6"), new Guid("50bbeb2b-f32c-4b01-806f-b55ed37991c6"), 121 }
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

            migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[spGetMostWins]
              AS
              select Top 3 *
              from tblUser
              order by GamesWon desc");
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
