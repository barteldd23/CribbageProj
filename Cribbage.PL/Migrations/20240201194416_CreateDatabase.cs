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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserGame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblUserGame_tblGame_GameId",
                        column: x => x.GameId,
                        principalTable: "tblGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblUserGame_tblUser_UserId",
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
                    { new Guid("29d2cd5c-8128-4788-8bbe-3b09637b1d97"), new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("46fc6823-3e11-4da9-b3f7-3c95318512b8"), 121, new Guid("49bfb501-ea23-40be-980c-cdfd9edfdde6"), 85, new Guid("46fc6823-3e11-4da9-b3f7-3c95318512b8") },
                    { new Guid("3c0970e7-020b-4744-bfa1-7c9c1f9ddf8f"), new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e946c4fe-483b-4afb-b645-13f29d741f0d"), 121, new Guid("46fc6823-3e11-4da9-b3f7-3c95318512b8"), 50, new Guid("e946c4fe-483b-4afb-b645-13f29d741f0d") },
                    { new Guid("84f21609-94ce-46fd-b021-ab918a671119"), new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("49bfb501-ea23-40be-980c-cdfd9edfdde6"), 121, new Guid("46fc6823-3e11-4da9-b3f7-3c95318512b8"), 70, new Guid("49bfb501-ea23-40be-980c-cdfd9edfdde6") },
                    { new Guid("adec168b-8b16-4b06-ae18-2e27837fca20"), new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("46fc6823-3e11-4da9-b3f7-3c95318512b8"), 90, new Guid("8a7d4da0-8e41-4894-a6e7-6773c99c6967"), 121, new Guid("8a7d4da0-8e41-4894-a6e7-6773c99c6967") }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "AvgPtsPerGame", "DisplayName", "Email", "FirstName", "GamesStarted", "LastName", "Losses", "Password", "WinStreak", "Wins" },
                values: new object[,]
                {
                    { new Guid("46fc6823-3e11-4da9-b3f7-3c95318512b8"), 82.75, "GamesRCool", "cards@me.com", "Kelly", 4, "Bot", 3, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("49bfb501-ea23-40be-980c-cdfd9edfdde6"), 103.0, "CribbageBox", "fun@yahoo.com", "Peter", 2, "Parker", 1, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("8a7d4da0-8e41-4894-a6e7-6773c99c6967"), 121.0, "Testing", "tester@gmail.com", "Test", 1, "Tester", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("e946c4fe-483b-4afb-b645-13f29d741f0d"), 121.0, "CardMaster", "cribbage@game.com", "Joe", 1, "Smith", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 }
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
