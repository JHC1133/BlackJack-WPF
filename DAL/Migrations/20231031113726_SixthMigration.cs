using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SixthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameStatistics");

            migrationBuilder.AddColumn<string>(
                name: "DealerName",
                table: "Games",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DealerStatisticsName",
                table: "Games",
                type: "nvarchar(450)",
                nullable: true);

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
                name: "IX_Games_DealerName",
                table: "Games",
                column: "DealerName");

            migrationBuilder.CreateIndex(
                name: "IX_Games_DealerStatisticsName",
                table: "Games",
                column: "DealerStatisticsName");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayerStatisticsIntermediary_PlayerName",
                table: "GamePlayerStatisticsIntermediary",
                column: "PlayerName");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_DealerStatistics_DealerName",
                table: "Games",
                column: "DealerName",
                principalTable: "DealerStatistics",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_DealerStatistics_DealerStatisticsName",
                table: "Games",
                column: "DealerStatisticsName",
                principalTable: "DealerStatistics",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_DealerStatistics_DealerName",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_DealerStatistics_DealerStatisticsName",
                table: "Games");

            migrationBuilder.DropTable(
                name: "GamePlayerStatisticsIntermediary");

            migrationBuilder.DropIndex(
                name: "IX_Games_DealerName",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_DealerStatisticsName",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DealerName",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DealerStatisticsName",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GameStatistics",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "int", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DealerName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatistics", x => new { x.GameID, x.PlayerName, x.DealerName });
                    table.ForeignKey(
                        name: "FK_GameStatistics_DealerStatistics_DealerName",
                        column: x => x.DealerName,
                        principalTable: "DealerStatistics",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStatistics_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStatistics_PlayerStatistics_PlayerName",
                        column: x => x.PlayerName,
                        principalTable: "PlayerStatistics",
                        principalColumn: "PlayerName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistics_DealerName",
                table: "GameStatistics",
                column: "DealerName");

            migrationBuilder.CreateIndex(
                name: "IX_GameStatistics_PlayerName",
                table: "GameStatistics",
                column: "PlayerName");
        }
    }
}
