using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTraderDataHelperAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixWeeklyAverageTimeIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekNumber",
                table: "WeeklyAverages");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "WeeklyAverages");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Time",
                table: "WeeklyAverages",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "WeeklyAverages");

            migrationBuilder.AddColumn<int>(
                name: "WeekNumber",
                table: "WeeklyAverages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "WeeklyAverages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
