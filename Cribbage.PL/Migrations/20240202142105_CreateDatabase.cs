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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserGame_Id", x => x.Id);
                    table.ForeignKey(
                        name: "tblUserGame_GameId",
                        column: x => x.GameId,
                        principalTable: "tblGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "tblUserGame_UserId",
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
                    { new Guid("0e237c3c-28e1-436f-a6e6-88e4e439a79d"), new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2d50981f-adbb-462f-a6e8-a155cc0f5270"), 121, new Guid("82270c89-705f-4f6e-9cf3-a8976ae1de73"), 70, new Guid("2d50981f-adbb-462f-a6e8-a155cc0f5270") },
                    { new Guid("0e57bb55-6dab-42be-baa0-d7a719d25d7f"), new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("82270c89-705f-4f6e-9cf3-a8976ae1de73"), 121, new Guid("2d50981f-adbb-462f-a6e8-a155cc0f5270"), 85, new Guid("82270c89-705f-4f6e-9cf3-a8976ae1de73") },
                    { new Guid("13150633-9f13-4194-b87b-9870beb79473"), new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cc7fa578-05af-40be-a042-446e5bc1e35a"), 121, new Guid("82270c89-705f-4f6e-9cf3-a8976ae1de73"), 50, new Guid("cc7fa578-05af-40be-a042-446e5bc1e35a") },
                    { new Guid("53d2a424-a192-4dd4-bf97-f0a492d53409"), new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("82270c89-705f-4f6e-9cf3-a8976ae1de73"), 90, new Guid("73aa110f-6999-4361-840a-b432c21fb8e9"), 121, new Guid("73aa110f-6999-4361-840a-b432c21fb8e9") }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "AvgPtsPerGame", "DisplayName", "Email", "FirstName", "GamesStarted", "LastName", "Losses", "Password", "WinStreak", "Wins" },
                values: new object[,]
                {
                    { new Guid("2d50981f-adbb-462f-a6e8-a155cc0f5270"), 103.0, "CribbageBox", "fun@yahoo.com", "Peter", 2, "Parker", 1, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("73aa110f-6999-4361-840a-b432c21fb8e9"), 121.0, "Testing", "tester@gmail.com", "Test", 1, "Tester", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("82270c89-705f-4f6e-9cf3-a8976ae1de73"), 82.75, "GamesRCool", "cards@me.com", "Kelly", 4, "Bot", 3, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("cc7fa578-05af-40be-a042-446e5bc1e35a"), 121.0, "CardMaster", "cribbage@game.com", "Joe", 1, "Smith", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "UserId" },
                values: new object[,]
                {
                    { -99, new Guid("0e237c3c-28e1-436f-a6e6-88e4e439a79d"), new Guid("2d50981f-adbb-462f-a6e8-a155cc0f5270") },
                    { -98, new Guid("53d2a424-a192-4dd4-bf97-f0a492d53409"), new Guid("73aa110f-6999-4361-840a-b432c21fb8e9") },
                    { -97, new Guid("0e57bb55-6dab-42be-baa0-d7a719d25d7f"), new Guid("82270c89-705f-4f6e-9cf3-a8976ae1de73") },
                    { -96, new Guid("13150633-9f13-4194-b87b-9870beb79473"), new Guid("cc7fa578-05af-40be-a042-446e5bc1e35a") }
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
