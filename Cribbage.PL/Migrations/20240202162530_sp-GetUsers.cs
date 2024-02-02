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
            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("001ac473-be2a-4909-ad85-3b57c73f39ff"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("21fb24d3-cdb5-47f1-aa48-a8ff56d4d7ce"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("5ed24903-f406-496e-85a2-c2d9c2bdd353"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("d3737424-0c85-4516-9022-341371195965"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("242a8880-a784-4caa-a914-599418ba0dee"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("2cf37fd1-6663-4b6d-ba9e-4f320bcd0736"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("dee8d805-06d2-4f58-887d-033db2d3f097"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("fdff717a-9025-4a3f-9021-d7ebf420ee29"));

            migrationBuilder.InsertData(
                table: "tblGame",
                columns: new[] { "Id", "Date", "Player_1_Id", "Player_1_Score", "Player_2_Id", "Player_2_Score", "Winner" },
                values: new object[,]
                {
                    { new Guid("239f90bb-f639-43ca-b0e6-eb26218656df"), new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8767e911-ae32-48ab-a0ce-9e3c2ad07c1d"), 121, new Guid("329ae371-22b0-420e-be50-248157a2b083"), 50, new Guid("8767e911-ae32-48ab-a0ce-9e3c2ad07c1d") },
                    { new Guid("4bd2973f-07a5-42d3-ac81-99411ef2facd"), new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("329ae371-22b0-420e-be50-248157a2b083"), 121, new Guid("20425efd-8ece-47c9-90b7-e2e9a0df1686"), 85, new Guid("329ae371-22b0-420e-be50-248157a2b083") },
                    { new Guid("4f513ed2-a3b1-4161-9eab-66b16fbde479"), new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("20425efd-8ece-47c9-90b7-e2e9a0df1686"), 121, new Guid("329ae371-22b0-420e-be50-248157a2b083"), 70, new Guid("20425efd-8ece-47c9-90b7-e2e9a0df1686") },
                    { new Guid("bf356195-3c43-4285-b54e-22587b1ea7e2"), new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("329ae371-22b0-420e-be50-248157a2b083"), 90, new Guid("022920f8-16d2-4050-8d65-c104b8d3e637"), 121, new Guid("022920f8-16d2-4050-8d65-c104b8d3e637") }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "AvgPtsPerGame", "DisplayName", "Email", "FirstName", "GamesStarted", "LastName", "Losses", "Password", "WinStreak", "Wins" },
                values: new object[,]
                {
                    { new Guid("022920f8-16d2-4050-8d65-c104b8d3e637"), 121.0, "Testing", "tester@gmail.com", "Test", 1, "Tester", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("20425efd-8ece-47c9-90b7-e2e9a0df1686"), 103.0, "CribbageBox", "fun@yahoo.com", "Peter", 2, "Parker", 1, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("329ae371-22b0-420e-be50-248157a2b083"), 82.75, "GamesRCool", "cards@me.com", "Kelly", 4, "Bot", 3, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("8767e911-ae32-48ab-a0ce-9e3c2ad07c1d"), 121.0, "CardMaster", "cribbage@game.com", "Joe", 1, "Smith", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 }
                });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -99,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("4f513ed2-a3b1-4161-9eab-66b16fbde479"), new Guid("20425efd-8ece-47c9-90b7-e2e9a0df1686") });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -98,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("bf356195-3c43-4285-b54e-22587b1ea7e2"), new Guid("022920f8-16d2-4050-8d65-c104b8d3e637") });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -97,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("4bd2973f-07a5-42d3-ac81-99411ef2facd"), new Guid("329ae371-22b0-420e-be50-248157a2b083") });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -96,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("239f90bb-f639-43ca-b0e6-eb26218656df"), new Guid("8767e911-ae32-48ab-a0ce-9e3c2ad07c1d") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("239f90bb-f639-43ca-b0e6-eb26218656df"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("4bd2973f-07a5-42d3-ac81-99411ef2facd"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("4f513ed2-a3b1-4161-9eab-66b16fbde479"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("bf356195-3c43-4285-b54e-22587b1ea7e2"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("022920f8-16d2-4050-8d65-c104b8d3e637"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("20425efd-8ece-47c9-90b7-e2e9a0df1686"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("329ae371-22b0-420e-be50-248157a2b083"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("8767e911-ae32-48ab-a0ce-9e3c2ad07c1d"));

            migrationBuilder.InsertData(
                table: "tblGame",
                columns: new[] { "Id", "Date", "Player_1_Id", "Player_1_Score", "Player_2_Id", "Player_2_Score", "Winner" },
                values: new object[,]
                {
                    { new Guid("001ac473-be2a-4909-ad85-3b57c73f39ff"), new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2cf37fd1-6663-4b6d-ba9e-4f320bcd0736"), 121, new Guid("242a8880-a784-4caa-a914-599418ba0dee"), 50, new Guid("2cf37fd1-6663-4b6d-ba9e-4f320bcd0736") },
                    { new Guid("21fb24d3-cdb5-47f1-aa48-a8ff56d4d7ce"), new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("242a8880-a784-4caa-a914-599418ba0dee"), 90, new Guid("dee8d805-06d2-4f58-887d-033db2d3f097"), 121, new Guid("dee8d805-06d2-4f58-887d-033db2d3f097") },
                    { new Guid("5ed24903-f406-496e-85a2-c2d9c2bdd353"), new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("242a8880-a784-4caa-a914-599418ba0dee"), 121, new Guid("fdff717a-9025-4a3f-9021-d7ebf420ee29"), 85, new Guid("242a8880-a784-4caa-a914-599418ba0dee") },
                    { new Guid("d3737424-0c85-4516-9022-341371195965"), new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("fdff717a-9025-4a3f-9021-d7ebf420ee29"), 121, new Guid("242a8880-a784-4caa-a914-599418ba0dee"), 70, new Guid("fdff717a-9025-4a3f-9021-d7ebf420ee29") }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "AvgPtsPerGame", "DisplayName", "Email", "FirstName", "GamesStarted", "LastName", "Losses", "Password", "WinStreak", "Wins" },
                values: new object[,]
                {
                    { new Guid("242a8880-a784-4caa-a914-599418ba0dee"), 82.75, "GamesRCool", "cards@me.com", "Kelly", 4, "Bot", 3, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("2cf37fd1-6663-4b6d-ba9e-4f320bcd0736"), 121.0, "CardMaster", "cribbage@game.com", "Joe", 1, "Smith", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("dee8d805-06d2-4f58-887d-033db2d3f097"), 121.0, "Testing", "tester@gmail.com", "Test", 1, "Tester", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("fdff717a-9025-4a3f-9021-d7ebf420ee29"), 103.0, "CribbageBox", "fun@yahoo.com", "Peter", 2, "Parker", 1, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 }
                });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -99,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("d3737424-0c85-4516-9022-341371195965"), new Guid("fdff717a-9025-4a3f-9021-d7ebf420ee29") });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -98,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("21fb24d3-cdb5-47f1-aa48-a8ff56d4d7ce"), new Guid("dee8d805-06d2-4f58-887d-033db2d3f097") });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -97,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("5ed24903-f406-496e-85a2-c2d9c2bdd353"), new Guid("242a8880-a784-4caa-a914-599418ba0dee") });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -96,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("001ac473-be2a-4909-ad85-3b57c73f39ff"), new Guid("2cf37fd1-6663-4b6d-ba9e-4f320bcd0736") });
        }
    }
}
