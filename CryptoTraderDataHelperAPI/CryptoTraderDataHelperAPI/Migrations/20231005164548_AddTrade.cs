using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTraderDataHelperAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    SymbolId = table.Column<int>(type: "INTEGER", nullable: false),
                    MinutelyAverageId = table.Column<int>(type: "INTEGER", nullable: true),
                    DailyAverageId = table.Column<int>(type: "INTEGER", nullable: true),
                    WeeklyAverageId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trades_DailyAverages_DailyAverageId",
                        column: x => x.DailyAverageId,
                        principalTable: "DailyAverages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trades_MinutelyAverages_MinutelyAverageId",
                        column: x => x.MinutelyAverageId,
                        principalTable: "MinutelyAverages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trades_Symbols_SymbolId",
                        column: x => x.SymbolId,
                        principalTable: "Symbols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trades_WeeklyAverages_WeeklyAverageId",
                        column: x => x.WeeklyAverageId,
                        principalTable: "WeeklyAverages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trades_DailyAverageId",
                table: "Trades",
                column: "DailyAverageId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_MinutelyAverageId",
                table: "Trades",
                column: "MinutelyAverageId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_SymbolId",
                table: "Trades",
                column: "SymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_WeeklyAverageId",
                table: "Trades",
                column: "WeeklyAverageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trades");
        }
    }
}
