using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Magic8Ball.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class data_seeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Answer",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Message", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6649), true, "It is certain.", 0, null },
                    { 2, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6656), true, "It is decidedly so.", 0, null },
                    { 3, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6657), true, "Without a doubt.", 0, null },
                    { 4, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6658), true, "Yes definitely.", 0, null },
                    { 5, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6658), true, "You may rely on it.", 0, null },
                    { 6, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6659), true, "As I see it, yes.", 0, null },
                    { 7, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6660), true, "Most likely.", 0, null },
                    { 8, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6661), true, "Outlook good.", 0, null },
                    { 9, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6662), true, "Yes.", 0, null },
                    { 10, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6662), true, "Signs point to yes.", 0, null },
                    { 11, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6663), true, "Reply hazy, try again.", 1, null },
                    { 12, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6664), true, "Ask again later.", 1, null },
                    { 13, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6665), true, "Better not tell you now.", 1, null },
                    { 14, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6666), true, "Cannot predict now.", 1, null },
                    { 15, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6666), true, "Concentrate and ask again.", 1, null },
                    { 16, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6667), true, "Don't count on it.", 2, null },
                    { 17, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6668), true, "My reply is no.", 2, null },
                    { 18, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6669), true, "My sources say no.", 2, null },
                    { 19, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6670), true, "Outlook not so good.", 2, null },
                    { 20, new DateTime(2024, 5, 22, 2, 45, 14, 252, DateTimeKind.Utc).AddTicks(6670), true, "Very doubtful.", 2, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Answer",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
