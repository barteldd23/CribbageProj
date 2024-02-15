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
                    { new Guid("33db8313-0441-4e40-9096-dce7d7c097a2"), true, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test5", new Guid("bbec4de0-524c-440e-8598-3cda26b298bc") },
                    { new Guid("776a75b7-1a9a-474d-bd41-8626ea2cc461"), true, new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test3", new Guid("08fc2f13-754c-46ae-adea-9188877593d2") },
                    { new Guid("8122b07d-74f4-47f2-b10b-3feb6f0a9e7a"), true, new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test1", new Guid("bb6a24a9-2aab-43e3-bc3d-3c0235a45ae6") },
                    { new Guid("c332ff9d-cd0f-4c33-8041-f27d8f80fd12"), true, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test4", new Guid("bbec4de0-524c-440e-8598-3cda26b298bc") },
                    { new Guid("f3753036-2b1c-47e0-9fa2-df8d043ef5e4"), true, new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test2", new Guid("975a40e0-9331-4f27-9154-d3f09f28e403") }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "AvgHandScore", "AvgPtsPerGame", "DisplayName", "Email", "FirstName", "GamesLost", "GamesStarted", "GamesWon", "LastName", "Password", "WinStreak" },
                values: new object[,]
                {
                    { new Guid("05258f2e-a384-49a9-920a-14ff7db49cec"), 5.0, 50.0, "Computer", "computer@computer.com", "Computer", 0, 1, 0, "Computer", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 0 },
                    { new Guid("08fc2f13-754c-46ae-adea-9188877593d2"), 8.0, 82.75, "GamesRCool", "cards@me.com", "Kelly", 3, 4, 1, "Bot", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1 },
                    { new Guid("975a40e0-9331-4f27-9154-d3f09f28e403"), 20.0, 121.0, "Testing", "tester@gmail.com", "Test", 0, 1, 1, "Tester", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1 },
                    { new Guid("bb6a24a9-2aab-43e3-bc3d-3c0235a45ae6"), 10.0, 103.0, "CribbageBox", "fun@yahoo.com", "Peter", 1, 2, 1, "Parker", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1 },
                    { new Guid("bbec4de0-524c-440e-8598-3cda26b298bc"), 15.0, 121.0, "CardMaster", "cribbage@game.com", "Joe", 0, 1, 2, "Smith", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 2 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "PlayerId", "PlayerScore" },
                values: new object[,]
                {
                    { new Guid("147f71fa-6cef-4673-b975-9e223ccf472b"), new Guid("8122b07d-74f4-47f2-b10b-3feb6f0a9e7a"), new Guid("bb6a24a9-2aab-43e3-bc3d-3c0235a45ae6"), 121 },
                    { new Guid("1d1887e5-5d78-46db-abc5-22dd14000bc0"), new Guid("c332ff9d-cd0f-4c33-8041-f27d8f80fd12"), new Guid("bbec4de0-524c-440e-8598-3cda26b298bc"), 121 },
                    { new Guid("3b3a9d10-88bf-4fb3-9874-9196799c5771"), new Guid("33db8313-0441-4e40-9096-dce7d7c097a2"), new Guid("bbec4de0-524c-440e-8598-3cda26b298bc"), 121 },
                    { new Guid("3fec4814-36b9-46ff-9fc6-8bdfcc7291ed"), new Guid("8122b07d-74f4-47f2-b10b-3feb6f0a9e7a"), new Guid("08fc2f13-754c-46ae-adea-9188877593d2"), 70 },
                    { new Guid("62e36f09-4d6e-444e-b44e-766a55e86585"), new Guid("776a75b7-1a9a-474d-bd41-8626ea2cc461"), new Guid("08fc2f13-754c-46ae-adea-9188877593d2"), 121 },
                    { new Guid("70b7534c-76f7-4af9-81c0-2867a9c0b8b1"), new Guid("c332ff9d-cd0f-4c33-8041-f27d8f80fd12"), new Guid("08fc2f13-754c-46ae-adea-9188877593d2"), 50 },
                    { new Guid("7e53c97e-1ad9-4d4a-a2b8-9bb3df845c87"), new Guid("33db8313-0441-4e40-9096-dce7d7c097a2"), new Guid("05258f2e-a384-49a9-920a-14ff7db49cec"), 50 },
                    { new Guid("c4e91afc-d4b5-4534-ad45-510e756708dc"), new Guid("776a75b7-1a9a-474d-bd41-8626ea2cc461"), new Guid("bb6a24a9-2aab-43e3-bc3d-3c0235a45ae6"), 85 },
                    { new Guid("e08b33dc-8b5e-452e-9e97-2a72b7658e87"), new Guid("f3753036-2b1c-47e0-9fa2-df8d043ef5e4"), new Guid("08fc2f13-754c-46ae-adea-9188877593d2"), 90 },
                    { new Guid("f2e31867-214f-4aa9-9678-bc6b50dc2313"), new Guid("f3753036-2b1c-47e0-9fa2-df8d043ef5e4"), new Guid("975a40e0-9331-4f27-9154-d3f09f28e403"), 121 }
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
