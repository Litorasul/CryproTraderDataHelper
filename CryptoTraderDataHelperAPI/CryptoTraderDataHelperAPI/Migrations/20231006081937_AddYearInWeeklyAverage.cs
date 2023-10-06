using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTraderDataHelperAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddYearInWeeklyAverage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "WeeklyAverages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "WeeklyAverages");
        }
    }
}
