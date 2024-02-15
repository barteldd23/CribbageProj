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
                    { new Guid("3e590a9b-eac9-43e6-9b65-82bed9972168"), true, new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test3", new Guid("507df959-7628-4162-9547-409356b236d9") },
                    { new Guid("8cbac0ec-d8fb-4282-b150-e60541328d92"), true, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test4", new Guid("baa7b930-1507-421b-91e6-3d81d9d413d8") },
                    { new Guid("b6bb3222-f678-4f22-810b-5daa724184fb"), true, new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test1", new Guid("3cd30b17-22a5-40c5-b988-e1c5baffd355") },
                    { new Guid("de015f28-6eac-4a06-beb8-ebe9435d8397"), true, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test5", new Guid("baa7b930-1507-421b-91e6-3d81d9d413d8") },
                    { new Guid("f333a17a-8a0b-4a0c-aafa-533cd111be1f"), true, new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test2", new Guid("ae29061b-939d-4cad-8c69-2dde184dc1dc") }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "AvgPtsPerGame", "DisplayName", "Email", "FirstName", "GamesLost", "GamesStarted", "GamesWon", "LastName", "Password", "WinStreak" },
                values: new object[,]
                {
                    { new Guid("3cd30b17-22a5-40c5-b988-e1c5baffd355"), 103.0, "CribbageBox", "fun@yahoo.com", "Peter", 1, 2, 1, "Parker", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1 },
                    { new Guid("507df959-7628-4162-9547-409356b236d9"), 82.75, "GamesRCool", "cards@me.com", "Kelly", 3, 4, 1, "Bot", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1 },
                    { new Guid("ae29061b-939d-4cad-8c69-2dde184dc1dc"), 121.0, "Testing", "tester@gmail.com", "Test", 0, 1, 1, "Tester", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1 },
                    { new Guid("b3af847f-e0fb-4f1e-be8e-7eea7e00f053"), 50.0, "Computer", "computer@computer.com", "Computer", 0, 1, 0, "Computer", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 0 },
                    { new Guid("baa7b930-1507-421b-91e6-3d81d9d413d8"), 121.0, "CardMaster", "cribbage@game.com", "Joe", 0, 1, 2, "Smith", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 2 }
                });

            migrationBuilder.InsertData(
                table: "tblUserGame",
                columns: new[] { "Id", "GameId", "PlayerId", "PlayerScore" },
                values: new object[,]
                {
                    { new Guid("26f4bff3-7153-4b61-ab04-9fa319d55a83"), new Guid("f333a17a-8a0b-4a0c-aafa-533cd111be1f"), new Guid("ae29061b-939d-4cad-8c69-2dde184dc1dc"), 121 },
                    { new Guid("2c31a158-5cdc-4dd6-ad8d-ee864794bba3"), new Guid("de015f28-6eac-4a06-beb8-ebe9435d8397"), new Guid("b3af847f-e0fb-4f1e-be8e-7eea7e00f053"), 50 },
                    { new Guid("2ff44603-0efc-4a2f-915d-46b107bf5454"), new Guid("b6bb3222-f678-4f22-810b-5daa724184fb"), new Guid("507df959-7628-4162-9547-409356b236d9"), 70 },
                    { new Guid("32582047-9686-4c96-a16c-7f70e2b3e290"), new Guid("3e590a9b-eac9-43e6-9b65-82bed9972168"), new Guid("3cd30b17-22a5-40c5-b988-e1c5baffd355"), 85 },
                    { new Guid("345becdf-78aa-4fc5-aa09-09b2cf397de1"), new Guid("f333a17a-8a0b-4a0c-aafa-533cd111be1f"), new Guid("507df959-7628-4162-9547-409356b236d9"), 90 },
                    { new Guid("473b8e26-228c-4c92-a819-dfaa545685cb"), new Guid("8cbac0ec-d8fb-4282-b150-e60541328d92"), new Guid("baa7b930-1507-421b-91e6-3d81d9d413d8"), 121 },
                    { new Guid("9a62c722-9f81-49ee-a3c2-6189dba0e06c"), new Guid("de015f28-6eac-4a06-beb8-ebe9435d8397"), new Guid("baa7b930-1507-421b-91e6-3d81d9d413d8"), 121 },
                    { new Guid("d5c64d98-194f-4ea3-b95b-74323cfea3ae"), new Guid("3e590a9b-eac9-43e6-9b65-82bed9972168"), new Guid("507df959-7628-4162-9547-409356b236d9"), 121 },
                    { new Guid("ec89bcb1-cc5c-4309-847f-7e4484517afe"), new Guid("8cbac0ec-d8fb-4282-b150-e60541328d92"), new Guid("507df959-7628-4162-9547-409356b236d9"), 50 },
                    { new Guid("f431db2f-4bf7-4339-83e8-262bfcadf9cd"), new Guid("b6bb3222-f678-4f22-810b-5daa724184fb"), new Guid("3cd30b17-22a5-40c5-b988-e1c5baffd355"), 121 }
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
