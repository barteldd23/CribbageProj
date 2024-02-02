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
                    { new Guid("1e3bf796-4f18-49c5-8378-e185d41b8990"), new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1f9c4ba8-3fd3-47e0-994b-d770561f6759"), 121, new Guid("b17eef8c-98f6-40d4-892a-e6eaf69fb981"), 85, new Guid("1f9c4ba8-3fd3-47e0-994b-d770561f6759") },
                    { new Guid("41ebb35f-6aac-4777-b9f2-7a957174cc82"), new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b17eef8c-98f6-40d4-892a-e6eaf69fb981"), 121, new Guid("1f9c4ba8-3fd3-47e0-994b-d770561f6759"), 70, new Guid("b17eef8c-98f6-40d4-892a-e6eaf69fb981") },
                    { new Guid("4667fe20-59da-4ad8-8c7f-075b45fdf640"), new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d58ab8af-84fb-417f-af19-6a2d6ae36619"), 121, new Guid("1f9c4ba8-3fd3-47e0-994b-d770561f6759"), 50, new Guid("d58ab8af-84fb-417f-af19-6a2d6ae36619") },
                    { new Guid("eb62f38d-4e79-4616-9bae-f4a061206009"), new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1f9c4ba8-3fd3-47e0-994b-d770561f6759"), 90, new Guid("4f80cec3-f8da-4264-b048-644b5ad75916"), 121, new Guid("4f80cec3-f8da-4264-b048-644b5ad75916") }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "AvgPtsPerGame", "DisplayName", "Email", "FirstName", "GamesStarted", "LastName", "Losses", "Password", "WinStreak", "Wins" },
                values: new object[,]
                {
                    { new Guid("1f9c4ba8-3fd3-47e0-994b-d770561f6759"), 82.75, "GamesRCool", "cards@me.com", "Kelly", 4, "Bot", 3, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("4f80cec3-f8da-4264-b048-644b5ad75916"), 121.0, "Testing", "tester@gmail.com", "Test", 1, "Tester", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("b17eef8c-98f6-40d4-892a-e6eaf69fb981"), 103.0, "CribbageBox", "fun@yahoo.com", "Peter", 2, "Parker", 1, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("d58ab8af-84fb-417f-af19-6a2d6ae36619"), 121.0, "CardMaster", "cribbage@game.com", "Joe", 1, "Smith", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "UserId" },
                values: new object[,]
                {
                    { -99, new Guid("41ebb35f-6aac-4777-b9f2-7a957174cc82"), new Guid("b17eef8c-98f6-40d4-892a-e6eaf69fb981") },
                    { -98, new Guid("eb62f38d-4e79-4616-9bae-f4a061206009"), new Guid("4f80cec3-f8da-4264-b048-644b5ad75916") },
                    { -97, new Guid("1e3bf796-4f18-49c5-8378-e185d41b8990"), new Guid("1f9c4ba8-3fd3-47e0-994b-d770561f6759") },
                    { -96, new Guid("4667fe20-59da-4ad8-8c7f-075b45fdf640"), new Guid("d58ab8af-84fb-417f-af19-6a2d6ae36619") }
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
