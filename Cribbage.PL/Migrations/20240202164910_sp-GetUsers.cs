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
            var sp = @"CREATE PROCEDURE [dbo].[Getusers]
                    @FirstName varchar(max)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    select * from Students where FirstName = @FirstName 
                END";

            migrationBuilder.Sql(sp);

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("1e3bf796-4f18-49c5-8378-e185d41b8990"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("41ebb35f-6aac-4777-b9f2-7a957174cc82"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("4667fe20-59da-4ad8-8c7f-075b45fdf640"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("eb62f38d-4e79-4616-9bae-f4a061206009"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("1f9c4ba8-3fd3-47e0-994b-d770561f6759"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("4f80cec3-f8da-4264-b048-644b5ad75916"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("b17eef8c-98f6-40d4-892a-e6eaf69fb981"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("d58ab8af-84fb-417f-af19-6a2d6ae36619"));

            migrationBuilder.InsertData(
                table: "tblGame",
                columns: new[] { "Id", "Date", "Player_1_Id", "Player_1_Score", "Player_2_Id", "Player_2_Score", "Winner" },
                values: new object[,]
                {
                    { new Guid("360f8257-8302-4c92-b082-605b87ab8914"), new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("aaa90d91-bfe0-44d7-a728-950921c2039e"), 121, new Guid("b0f4fee8-0f51-4ff4-8b8b-d20c7d8edb1c"), 70, new Guid("aaa90d91-bfe0-44d7-a728-950921c2039e") },
                    { new Guid("b70d469c-f4f5-41f7-b082-9814be6265e3"), new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b0f4fee8-0f51-4ff4-8b8b-d20c7d8edb1c"), 90, new Guid("6290f320-8b42-4d4e-b950-49103e2aea0e"), 121, new Guid("6290f320-8b42-4d4e-b950-49103e2aea0e") },
                    { new Guid("ba24a216-76fe-4ae6-983d-acf1c30ca2de"), new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b9923279-35b3-498e-bc9e-20d53236a8b1"), 121, new Guid("b0f4fee8-0f51-4ff4-8b8b-d20c7d8edb1c"), 50, new Guid("b9923279-35b3-498e-bc9e-20d53236a8b1") },
                    { new Guid("fab60e12-04f3-4efe-97b2-fcac85ea7a8d"), new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b0f4fee8-0f51-4ff4-8b8b-d20c7d8edb1c"), 121, new Guid("aaa90d91-bfe0-44d7-a728-950921c2039e"), 85, new Guid("b0f4fee8-0f51-4ff4-8b8b-d20c7d8edb1c") }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "AvgPtsPerGame", "DisplayName", "Email", "FirstName", "GamesStarted", "LastName", "Losses", "Password", "WinStreak", "Wins" },
                values: new object[,]
                {
                    { new Guid("6290f320-8b42-4d4e-b950-49103e2aea0e"), 121.0, "Testing", "tester@gmail.com", "Test", 1, "Tester", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("aaa90d91-bfe0-44d7-a728-950921c2039e"), 103.0, "CribbageBox", "fun@yahoo.com", "Peter", 2, "Parker", 1, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("b0f4fee8-0f51-4ff4-8b8b-d20c7d8edb1c"), 82.75, "GamesRCool", "cards@me.com", "Kelly", 4, "Bot", 3, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 },
                    { new Guid("b9923279-35b3-498e-bc9e-20d53236a8b1"), 121.0, "CardMaster", "cribbage@game.com", "Joe", 1, "Smith", 0, "pYfdnNb8sO0FgS4H0MRSwLGOIME=", 1, 1 }
                });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -99,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("360f8257-8302-4c92-b082-605b87ab8914"), new Guid("aaa90d91-bfe0-44d7-a728-950921c2039e") });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -98,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("b70d469c-f4f5-41f7-b082-9814be6265e3"), new Guid("6290f320-8b42-4d4e-b950-49103e2aea0e") });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -97,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("fab60e12-04f3-4efe-97b2-fcac85ea7a8d"), new Guid("b0f4fee8-0f51-4ff4-8b8b-d20c7d8edb1c") });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -96,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("ba24a216-76fe-4ae6-983d-acf1c30ca2de"), new Guid("b9923279-35b3-498e-bc9e-20d53236a8b1") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("360f8257-8302-4c92-b082-605b87ab8914"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("b70d469c-f4f5-41f7-b082-9814be6265e3"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("ba24a216-76fe-4ae6-983d-acf1c30ca2de"));

            migrationBuilder.DeleteData(
                table: "tblGame",
                keyColumn: "Id",
                keyValue: new Guid("fab60e12-04f3-4efe-97b2-fcac85ea7a8d"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("6290f320-8b42-4d4e-b950-49103e2aea0e"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("aaa90d91-bfe0-44d7-a728-950921c2039e"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("b0f4fee8-0f51-4ff4-8b8b-d20c7d8edb1c"));

            migrationBuilder.DeleteData(
                table: "tblUser",
                keyColumn: "Id",
                keyValue: new Guid("b9923279-35b3-498e-bc9e-20d53236a8b1"));

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

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -99,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("41ebb35f-6aac-4777-b9f2-7a957174cc82"), new Guid("b17eef8c-98f6-40d4-892a-e6eaf69fb981") });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -98,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("eb62f38d-4e79-4616-9bae-f4a061206009"), new Guid("4f80cec3-f8da-4264-b048-644b5ad75916") });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -97,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("1e3bf796-4f18-49c5-8378-e185d41b8990"), new Guid("1f9c4ba8-3fd3-47e0-994b-d770561f6759") });

            migrationBuilder.UpdateData(
                table: "tblUserGame",
                keyColumn: "Id",
                keyValue: -96,
                columns: new[] { "GameId", "UserId" },
                values: new object[] { new Guid("4667fe20-59da-4ad8-8c7f-075b45fdf640"), new Guid("d58ab8af-84fb-417f-af19-6a2d6ae36619") });
        }
    }
}
