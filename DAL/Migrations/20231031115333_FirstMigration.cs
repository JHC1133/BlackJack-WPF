using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DealerStatistics",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Blackjacks = table.Column<int>(type: "int", nullable: false),
                    Busts = table.Column<int>(type: "int", nullable: false),
                    Ties = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealerStatistics", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStatistics",
                columns: table => new
                {
                    PlayerName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Blackjacks = table.Column<int>(type: "int", nullable: false),
                    Busts = table.Column<int>(type: "int", nullable: false),
                    Ties = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatistics", x => x.PlayerName);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatePlayed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DealerName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DealerStatisticsName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Games_DealerStatistics_DealerName",
                        column: x => x.DealerName,
                        principalTable: "DealerStatistics",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_DealerStatistics_DealerStatisticsName",
                        column: x => x.DealerStatisticsName,
                        principalTable: "DealerStatistics",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "GamePlayerStatisticsIntermediary",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "int", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GamePlayerStatisticsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayerStatisticsIntermediary", x => new { x.GameID, x.PlayerName });
                    table.ForeignKey(
                        name: "FK_GamePlayerStatisticsIntermediary_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlayerStatisticsIntermediary_PlayerStatistics_PlayerName",
                        column: x => x.PlayerName,
                        principalTable: "PlayerStatistics",
                        principalColumn: "PlayerName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayerStatisticsIntermediary_PlayerName",
                table: "GamePlayerStatisticsIntermediary",
                column: "PlayerName");

            migrationBuilder.CreateIndex(
                name: "IX_Games_DealerName",
                table: "Games",
                column: "DealerName");

            migrationBuilder.CreateIndex(
                name: "IX_Games_DealerStatisticsName",
                table: "Games",
                column: "DealerStatisticsName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlayerStatisticsIntermediary");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "PlayerStatistics");

            migrationBuilder.DropTable(
                name: "DealerStatistics");
        }
    }
}
