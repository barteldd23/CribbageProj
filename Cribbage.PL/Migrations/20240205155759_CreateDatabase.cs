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
                    { new Guid("1079eb15-54df-47fd-b40f-c71cbbcbfc3b"), new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("834fc2c5-1638-4593-a5e9-dea5d5d2d143"), 121, new Guid("501efe9c-fb72-4037-bb0f-feca082ba20b"), 85, new Guid("834fc2c5-1638-4593-a5e9-dea5d5d2d143") },
                    { new Guid("59f97341-0c60-4bb8-92b2-cedf364e4a77"), new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("501efe9c-fb72-4037-bb0f-feca082ba20b"), 121, new Guid("834fc2c5-1638-4593-a5e9-dea5d5d2d143"), 70, new Guid("501efe9c-fb72-4037-bb0f-feca082ba20b") },
                    { new Guid("696ed53e-5341-4474-a720-d0744adf43f7"), new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9a6a1873-0111-472e-aa05-12577d2cb9ae"), 121, new Guid("b0f1e70f-76e4-4206-ad5e-786c291ca264"), 50, new Guid("9a6a1873-0111-472e-aa05-12577d2cb9ae") },
                    { new Guid("b9f9a5a7-44bd-44ec-8ed6-65ce70242e1c"), new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9a6a1873-0111-472e-aa05-12577d2cb9ae"), 121, new Guid("834fc2c5-1638-4593-a5e9-dea5d5d2d143"), 50, new Guid("9a6a1873-0111-472e-aa05-12577d2cb9ae") },
                    { new Guid("d8848963-8adb-4f7d-b531-61fadfac947e"), new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("834fc2c5-1638-4593-a5e9-dea5d5d2d143"), 90, new Guid("29436414-985b-4181-9cf8-787bd3204d87"), 121, new Guid("29436414-985b-4181-9cf8-787bd3204d87") }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "AvgPtsPerGame", "DisplayName", "Email", "FirstName", "GamesStarted", "LastName", "Losses", "Password", "WinStreak", "Wins" },
                values: new object[,]
                {
                    { new Guid("29436414-985b-4181-9cf8-787bd3204d87"), 121.0, "Testing", "tester@gmail.com", "Test", 1, "Tester", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("501efe9c-fb72-4037-bb0f-feca082ba20b"), 103.0, "CribbageBox", "fun@yahoo.com", "Peter", 2, "Parker", 1, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("834fc2c5-1638-4593-a5e9-dea5d5d2d143"), 82.75, "GamesRCool", "cards@me.com", "Kelly", 4, "Bot", 3, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("9a6a1873-0111-472e-aa05-12577d2cb9ae"), 121.0, "CardMaster", "cribbage@game.com", "Joe", 1, "Smith", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 2, 2 },
                    { new Guid("b0f1e70f-76e4-4206-ad5e-786c291ca264"), 50.0, "Computer", "computer@computer.com", "Computer", 1, "Computer", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "UserId" },
                values: new object[,]
                {
                    { -99, new Guid("59f97341-0c60-4bb8-92b2-cedf364e4a77"), new Guid("501efe9c-fb72-4037-bb0f-feca082ba20b") },
                    { -98, new Guid("d8848963-8adb-4f7d-b531-61fadfac947e"), new Guid("29436414-985b-4181-9cf8-787bd3204d87") },
                    { -97, new Guid("1079eb15-54df-47fd-b40f-c71cbbcbfc3b"), new Guid("834fc2c5-1638-4593-a5e9-dea5d5d2d143") },
                    { -96, new Guid("b9f9a5a7-44bd-44ec-8ed6-65ce70242e1c"), new Guid("9a6a1873-0111-472e-aa05-12577d2cb9ae") },
                    { -95, new Guid("696ed53e-5341-4474-a720-d0744adf43f7"), new Guid("9a6a1873-0111-472e-aa05-12577d2cb9ae") }
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
